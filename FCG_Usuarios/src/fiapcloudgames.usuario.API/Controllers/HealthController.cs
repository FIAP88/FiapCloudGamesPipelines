using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Diagnostics;
using fiapcloudgames.usuario.API.Attributes;

namespace fiapcloudgames.usuario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymousApiKey]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Verifica o status de saúde da API
        /// </summary>
        /// <returns>Informações básicas de saúde da aplicação</returns>
        /// <response code="200">API está funcionando corretamente</response>
        [HttpGet]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var healthInfo = new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                MachineName = Environment.MachineName,
                ProcessId = Environment.ProcessId,
                Uptime = DateTime.UtcNow - Process.GetCurrentProcess().StartTime
            };

            _logger.LogInformation("Health check requested - Status: {Status}", healthInfo.Status);
            return Ok(healthInfo);
        }

        /// <summary>
        /// Verifica informações detalhadas da aplicação
        /// </summary>
        /// <returns>Informações completas do sistema e runtime</returns>
        /// <response code="200">Informações detalhadas da aplicação</response>
        [HttpGet("info")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GetInfo()
        {
            var appInfo = new
            {
                Application = new
                {
                    Name = "FIAP Cloud Games - Usuários API",
                    Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                    Description = "API para gerenciamento de usuários da plataforma FIAP Cloud Games"
                },
                Environment = new
                {
                    Name = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                    MachineName = Environment.MachineName,
                    ProcessorCount = Environment.ProcessorCount,
                    OSVersion = Environment.OSVersion.ToString(),
                    WorkingSet = GC.GetTotalMemory(false),
                    Framework = Environment.Version.ToString()
                },
                Runtime = new
                {
                    ProcessId = Environment.ProcessId,
                    StartTime = Process.GetCurrentProcess().StartTime,
                    Uptime = DateTime.UtcNow - Process.GetCurrentProcess().StartTime,
                    ThreadCount = Process.GetCurrentProcess().Threads.Count
                },
                Configuration = new
                {
                    ApiKeyEnabled = true,
                    RateLimitEnabled = true,
                    SecurityHeadersEnabled = true,
                    LoggingEnabled = true
                }
            };

            return Ok(appInfo);
        }
    }
}