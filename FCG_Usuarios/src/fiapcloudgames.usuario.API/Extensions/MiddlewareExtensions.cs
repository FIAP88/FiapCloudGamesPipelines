using fiapcloudgames.usuario.API.Middleware;

namespace fiapcloudgames.usuario.API.Extensions
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adiciona o middleware de logging de requisição/resposta com tratamento de exceções
        /// </summary>
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }

        /// <summary>
        /// Adiciona o middleware de rate limiting
        /// </summary>
        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RateLimitingMiddleware>();
        }

        /// <summary>
        /// Adiciona o middleware de headers de seguran�a
        /// </summary>
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityHeadersMiddleware>();
        }

        /// <summary>
        /// Adiciona o middleware de tratamento de erros JWT
        /// </summary>
        public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthenticationMiddleware>();
        }

        /// <summary>
        /// Adiciona todos os middlewares customizados da aplica��o na ordem correta
        /// </summary>
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder builder)
        {
            return builder
                .UseJwtAuthentication()         // Primeiro: tratamento de erros JWT
                .UseSecurityHeaders()           // Segundo: headers de seguran�a
                .UseRequestResponseLogging()    // Terceiro: logging (captura tudo)
                .UseRateLimiting();             // Quarto: rate limiting
        }
    }
}