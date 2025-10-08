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
        /// Verifica o status de sa�de da API
        /// </summary>
        /// <returns>Informa��es b�sicas de sa�de da aplica��o</returns>
        /// <response code="200">API est� funcionando corretamente</response>
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
        /// Verifica informa��es detalhadas da aplica��o
        /// </summary>
        /// <returns>Informa��es completas do sistema e runtime</returns>
        /// <response code="200">Informa��es detalhadas da aplica��o</response>
        [HttpGet("info")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GetInfo()
        {
            var appInfo = new
            {
                Application = new
                {
                    Name = "FIAP Cloud Games - Usu�rios API",
                    Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                    Description = "API para gerenciamento de usu�rios da plataforma FIAP Cloud Games"
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