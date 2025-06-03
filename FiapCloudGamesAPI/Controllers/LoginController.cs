using AutenticacaoEAutorizacaoCorreto.Services.IService;
using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Entidades;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesAPI.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Autenticação")]
    public class LoginController(
        AppDbContext context, 
        BaseLogger<Login> logger,
        ITokenService tokenService,
        IHttpContextAccessor httpContext,
        ICacheService cacheService) : BaseController<Login>(context, logger, httpContext)
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly ICacheService _cacheService = cacheService;


        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation("Realizar login de usuário")]
        public async Task<ActionResult> Login(string email, string senha)
        {
            Usuario usuario = await _context.Usuarios.Include(u => u.Perfil)
                    .ThenInclude(p => p.PerfilPermissoes)
                    .ThenInclude(p => p.Permissao)
                    .FirstOrDefaultAsync(user => user.Email == email);

            if (usuario == null) return BadRequest(new { message = "Usuario ou Senha incorreto." });

            var senhaValida = VerificarSenha(senha, usuario.HashSenha);

            if (!senhaValida) return BadRequest(new { message = "Usuario ou Senha incorreto." });

            var key = $"token{email}";

            var cachedToken = _cacheService.get(key);

            if (cachedToken != null) return Ok(cachedToken);

            var token = _tokenService.GerarToken(usuario);
            _cacheService.set(key, token);

            return Ok(token);
        }

        private bool VerificarSenha(string senha, string hashArmazenado)
        {
            byte[] hashBytes = Convert.FromBase64String(hashArmazenado);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(senha, salt, 100_000);
            byte[] hash = pbkdf2.GetBytes(32);

            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }

    }
}
