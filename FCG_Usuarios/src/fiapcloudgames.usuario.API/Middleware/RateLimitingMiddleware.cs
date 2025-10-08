using System.Collections.Concurrent;
using System.Net;

namespace fiapcloudgames.usuario.API.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private static readonly ConcurrentDictionary<string, ClientRequestInfo> _clients = new();
        private readonly int _maxRequests;
        private readonly TimeSpan _timeWindow;

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            
            // Configurações do rate limiting
            _maxRequests = configuration.GetValue<int>("RateLimit:MaxRequests", 100); // 100 requests por padrão
            var windowMinutes = configuration.GetValue<int>("RateLimit:WindowMinutes", 1); // 1 minuto por padrão
            _timeWindow = TimeSpan.FromMinutes(windowMinutes);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientId = GetClientIdentifier(context);
            var now = DateTime.UtcNow;

            // Limpar registros antigos
            CleanupOldEntries(now);

            var clientInfo = _clients.GetOrAdd(clientId, _ => new ClientRequestInfo());

            bool rateLimitExceeded = false;
            int remaining = 0;
            DateTime resetTime = now;

            lock (clientInfo)
            {
                // Remover requests antigas da janela de tempo
                clientInfo.Requests.RemoveAll(r => now - r > _timeWindow);

                // Verificar se excedeu o limite
                if (clientInfo.Requests.Count >= _maxRequests)
                {
                    rateLimitExceeded = true;
                }
                else
                {
                    // Adicionar request atual
                    clientInfo.Requests.Add(now);
                }

                // Calcular informações para headers
                remaining = Math.Max(0, _maxRequests - clientInfo.Requests.Count);
                var oldestRequest = clientInfo.Requests.Count > 0 ? clientInfo.Requests.Min() : now;
                resetTime = oldestRequest.Add(_timeWindow);
            }

            // Adicionar headers informativos (fora do lock)
            context.Response.Headers.Add("X-RateLimit-Limit", _maxRequests.ToString());
            context.Response.Headers.Add("X-RateLimit-Remaining", remaining.ToString());
            context.Response.Headers.Add("X-RateLimit-Reset", ((DateTimeOffset)resetTime).ToUnixTimeSeconds().ToString());

            if (rateLimitExceeded)
            {
                _logger.LogWarning("Rate limit exceeded for client: {ClientId}", clientId);
                
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Too many requests.");
                return;
            }

            await _next(context);
        }

        private string GetClientIdentifier(HttpContext context)
        {
            // Priorizar IP real do cliente (considerando proxies)
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',')[0].Trim();
            }

            var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(realIp))
            {
                return realIp;
            }

            // Fallback para IP da conexão
            return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }

        private void CleanupOldEntries(DateTime now)
        {
            var cutoff = now - _timeWindow;
            var keysToRemove = new List<string>();

            foreach (var kvp in _clients)
            {
                lock (kvp.Value)
                {
                    kvp.Value.Requests.RemoveAll(r => r < cutoff);
                    
                    // Remover clientes inativos
                    if (kvp.Value.Requests.Count == 0 && now - kvp.Value.LastActivity > _timeWindow)
                    {
                        keysToRemove.Add(kvp.Key);
                    }
                    else
                    {
                        kvp.Value.LastActivity = now;
                    }
                }
            }

            foreach (var key in keysToRemove)
            {
                _clients.TryRemove(key, out _);
            }
        }
    }

    public class ClientRequestInfo
    {
        public List<DateTime> Requests { get; set; } = new();
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
    }
}