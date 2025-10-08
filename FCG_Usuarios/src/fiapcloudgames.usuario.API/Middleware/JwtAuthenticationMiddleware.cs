using System.Net;
using System.Text.Json;

namespace fiapcloudgames.usuario.API.Middleware
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtAuthenticationMiddleware> _logger;

        public JwtAuthenticationMiddleware(RequestDelegate next, ILogger<JwtAuthenticationMiddleware> logger)
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
                _logger.LogError(ex, "Erro durante o processamento da requisição");
                await HandleExceptionAsync(context, ex);
                return;
            }

            // Verificar se a resposta é 401 (Unauthorized) e não possui conteúdo
            if (context.Response.StatusCode == 401 && !context.Response.HasStarted)
            {
                await HandleUnauthorizedAsync(context);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                title = "Erro interno do servidor",
                status = context.Response.StatusCode,
                detail = "Ocorreu um erro inesperado. Tente novamente mais tarde."
            };

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }

        private async Task HandleUnauthorizedAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            
            string detail;
            if (string.IsNullOrEmpty(authHeader))
            {
                detail = "Token de autenticação não fornecido. Inclua o header 'Authorization: Bearer {token}' na requisição.";
            }
            else if (!authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                detail = "Formato do token inválido. Use o formato 'Bearer {token}' no header Authorization.";
            }
            else
            {
                detail = "Token JWT inválido ou expirado. Faça login novamente para obter um novo token.";
            }

            var response = new
            {
                type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                title = "Não autorizado",
                status = 401,
                detail = detail,
                instance = context.Request.Path.Value
            };

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}