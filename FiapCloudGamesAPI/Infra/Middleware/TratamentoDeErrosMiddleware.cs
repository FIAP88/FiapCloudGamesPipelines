using System.Net;
using System.Text.Json;

namespace FiapCloudGamesAPI.Infra.Middleware
{
    public class TratamentoDeErrosMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TratamentoDeErrosMiddleware> _logger;

        public TratamentoDeErrosMiddleware(RequestDelegate next, ILogger<TratamentoDeErrosMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new
            {
                error = "Ocorreu um erro inesperado.",
                detalhes = exception.Message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            _logger.LogError(exception, "Ocorreu um erro no processamento da requisição.");
            return context.Response.WriteAsync(result);
        }

       
    }
    public static class TratamentoDeErrosMiddlewareExtensions
    {
        public static IApplicationBuilder UseTratamentoDeErrosMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TratamentoDeErrosMiddleware>();
        }
    }
}
