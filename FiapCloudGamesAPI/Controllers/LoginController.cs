using AutenticacaoEAutorizacaoCorreto.Services.IService;
using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Entidades;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesAPI.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseControllerFiapCloudGames<Login>
     
    {
        private readonly ITokenService _tokenService;
        private readonly ICacheService _cacheService;

        public LoginController(
        AppDbContext context,
        BaseLogger<Login> logger,
        ITokenService tokenService,
        ICacheService cacheService) : base(context, logger)
        {
            _cacheService = cacheService;
            _tokenService = tokenService;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string email, string senha)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email == email);
            //var usuario = new Usuario { Nome = "Teste", Id = 1, PerfilId = 5 };

            if (usuario == null) return NotFound(new { message = "Usuario não encontrado" });

            //if (usuario.Senha != senha) return Ok("Email ou senha incorreto.");

            usuario.HashSenha = "";

            var key = $"token{email}";

            var cachedToken = _cacheService.get(key);

            if (cachedToken != null) return Ok(cachedToken);

            var token = _tokenService.GerarToken(usuario);
            _cacheService.set(key, token);

            return Ok(token);
        }
    }
}
