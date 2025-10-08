namespace fiapcloudgames.usuario.API.Middleware
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SecurityHeadersMiddleware> _logger;

        public SecurityHeadersMiddleware(RequestDelegate next, ILogger<SecurityHeadersMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Adicionar headers de segurança
            AddSecurityHeaders(context);

            // Processar CORS manualmente se necessário
            if (context.Request.Method == "OPTIONS")
            {
                await HandlePreflightRequest(context);
                return;
            }

            await _next(context);
        }

        private void AddSecurityHeaders(HttpContext context)
        {
            var response = context.Response;

            // Prevenir clickjacking
            if (!response.Headers.ContainsKey("X-Frame-Options"))
            {
                response.Headers.Add("X-Frame-Options", "DENY");
            }

            // Prevenir MIME type sniffing
            if (!response.Headers.ContainsKey("X-Content-Type-Options"))
            {
                response.Headers.Add("X-Content-Type-Options", "nosniff");
            }

            // Forçar HTTPS (apenas em produção)
            if (context.Request.IsHttps && !response.Headers.ContainsKey("Strict-Transport-Security"))
            {
                response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            }

            // Política de referrer
            if (!response.Headers.ContainsKey("Referrer-Policy"))
            {
                response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
            }

            // Política de permissões
            if (!response.Headers.ContainsKey("Permissions-Policy"))
            {
                response.Headers.Add("Permissions-Policy", "camera=(), microphone=(), geolocation=()");
            }

            // Content Security Policy básica
            if (!response.Headers.ContainsKey("Content-Security-Policy"))
            {
                response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline';");
            }

            // Remover headers que revelam informações do servidor
            response.Headers.Remove("Server");
            response.Headers.Remove("X-Powered-By");
            response.Headers.Remove("X-AspNet-Version");

            // Adicionar header personalizado da aplicação
            response.Headers.Add("X-API-Version", "1.0");
            response.Headers.Add("X-Application", "FIAP-CloudGames-Users");
        }

        private async Task HandlePreflightRequest(HttpContext context)
        {
            var origin = context.Request.Headers["Origin"].FirstOrDefault();
            
            // Lista de origens permitidas (configurável)
            var allowedOrigins = new[] { 
                "http://localhost:3000", 
                "http://localhost:4200", 
                "https://fiapcloudgames.com",
                "https://*.fiapcloudgames.com",
                "https://*.azurewebsites.net"
            };

            if (!string.IsNullOrEmpty(origin) && IsOriginAllowed(origin, allowedOrigins))
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", origin);
            }

            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            context.Response.Headers.Add("Access-Control-Allow-Headers", 
                "Content-Type, Authorization, X-API-Key, X-Requested-With, Accept, Origin");
            context.Response.Headers.Add("Access-Control-Max-Age", "86400"); // 24 horas

            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("");
        }

        private bool IsOriginAllowed(string origin, string[] allowedOrigins)
        {
            foreach (var allowedOrigin in allowedOrigins)
            {
                if (allowedOrigin.Contains("*"))
                {
                    // Suporte básico para wildcard
                    var pattern = allowedOrigin.Replace("*", ".*");
                    if (System.Text.RegularExpressions.Regex.IsMatch(origin, pattern))
                    {
                        return true;
                    }
                }
                else if (string.Equals(origin, allowedOrigin, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}