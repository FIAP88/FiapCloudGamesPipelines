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
    public class PermissoesMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissoesMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext, ITokenService tokenService, ICacheService cacheService, AppDbContext context)
        {
            var path = httpContext.Request.Path.Value;
            if (path != null && (path.StartsWith("/swagger") || path.StartsWith("/api/Login") ))
            {
                await _next(httpContext);
                return;
            }

            var token = httpContext.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                httpContext.Response.StatusCode = 401; // Unauthorized  
                await httpContext.Response.WriteAsync("Token não encontrado");
                return;
            }

            var usuarioId = tokenService.GetUsuarioId(token);
            Usuario usuario = await context.Usuarios.FindAsync(usuarioId);

            var permissaoService = new PermissaoService();
            if(!permissaoService.UsuarioTemPermissao(usuario, httpContext.Request))
            {
                httpContext.Response.StatusCode = 403; // Forbidden  
                await httpContext.Response.WriteAsync("Usuário não tem permissão para acessar este recurso");
                return;
            }

            // Adiciona o usuário ao contexto para uso posterior
            httpContext.Items["Usuario"] = usuario;
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.  
    public static class PermissoesMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermissoesMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermissoesMiddleware>();
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
