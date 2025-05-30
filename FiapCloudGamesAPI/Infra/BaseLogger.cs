using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.Infra
{
    public class BaseLogger<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly ICorrelationIdGenerator _correlationId;
        protected readonly AppDbContext _context;
        protected readonly Usuario? _usuario;
        private static string Entidade => typeof(T).Name;


        public BaseLogger(ILogger<T> logger, ICorrelationIdGenerator correlationId, AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _correlationId = correlationId;
            _context = context;
            _usuario = httpContextAccessor?.HttpContext?.Items["Usuario"] as Usuario;
        }

        public virtual void LogInformation(string message)
        {
            message = $"[CorrelationId: {_correlationId.Get()}] {Entidade}: {message}";
            _logger.LogInformation(message);
            _context.Logs.Add(new Log(message,_usuario?.Nome ?? string.Empty));
        }

        public virtual void LogError(string message)
        {
            message = $"[CorrelationId: {_correlationId.Get()}] {Entidade}: {message}";
            _logger.LogError(message);
            _context.Logs.Add(new Log(message, _usuario?.Nome ?? string.Empty));
        }

        public virtual void LogWarning(string message)
        {
            message = $"[CorrelationId: {_correlationId.Get()}] {Entidade}: {message}";
            _logger.LogWarning(message);
            _context.Logs.Add(new Log(message, _usuario?.Nome ?? string.Empty));
        }
    }
}
