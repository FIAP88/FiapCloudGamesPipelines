using AutenticacaoEAutorizacaoCorreto.Services.IService;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesAPI.Services.IService;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ICacheService _cacheService;
        public LoginController(ITokenService tokenService, ICacheService cacheService)
        {
            _tokenService = tokenService;
            _cacheService = cacheService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string email, string senha)
        {
            //var usuario = await _usuario.getUsuario(email);
            var usuario = new Usuario { Name = "Teste", UserId = 1, IdPerfil = 5 };

            if (usuario == null) return NotFound(new { message = "Usuario não encontrado" });

            //if (usuario.Senha != senha) return Ok("Email ou senha incorreto.");

            usuario.Senha = "";

            var key = "token";

            var cachedToken = _cacheService.get(key);

            if (cachedToken != null) return Ok(cachedToken);

            var token = _tokenService.GerarToken(usuario);
            _cacheService.set(key, token);

            return Ok(token);
        }
    }
}
