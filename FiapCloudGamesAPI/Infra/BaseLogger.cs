namespace FiapCloudGamesAPI.Infra
{
    public class BaseLogger<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly ICorrelationIdGenerator _correlationId;
        private string _entidade => typeof(T).Name;

        public BaseLogger(ILogger<T> logger, ICorrelationIdGenerator correlationId)
        {
            _logger = logger;
            _correlationId = correlationId;
        }

        public virtual void LogInformation(string message)
        {
            _logger.LogInformation($"[CorrelationId: {_correlationId.Get()}] {_entidade}: {message}");
        }

        public virtual void LogError(string message)
        {
            _logger.LogError($"[CorrelationId: {_correlationId.Get()}] {_entidade}: {message}");
        }

        public virtual void LogWarning(string message)
        {
            _logger.LogWarning($"[CorrelationId: {_correlationId.Get()}] {_entidade}: {message}");
        }
    }
}
