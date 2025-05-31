using AutenticacaoEAutorizacaoCorreto.Services.IService;
using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesAPI.Services;
using FiapCloudGamesAPI.Services.IService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FiapCloudGamesAPI.Infra.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project  
    public class InfoUsuarioMiddleware
    {
        private readonly RequestDelegate _next;

        public InfoUsuarioMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext, ITokenService tokenService, ICacheService cacheService, AppDbContext context)
        {
            var token = httpContext.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                var usuarioId = tokenService.GetUsuarioId(token);
                var usuario = await context.Usuarios.FindAsync(usuarioId);
                if (usuario != null)
                    httpContext.Items["Usuario"] = usuario;
            }
            
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.  
    public static class InfoUsuarioMiddlewareExtensions
    {
        public static IApplicationBuilder UseInfoUsuarioMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InfoUsuarioMiddleware>();
        }
    }

    // Static class to define the extension method  
    public static class HttpContextExtensions
    {
        public static string GetToken(this HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token))
                return string.Empty;
            return token.Replace("Bearer ", string.Empty);
        }
    }
}
