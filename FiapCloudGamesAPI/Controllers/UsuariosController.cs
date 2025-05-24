using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Entidades.Dtos;
using FiapCloudGamesAPI.Entidades.Requests;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "GerenciarUsuarios")]
    public class UsuariosController(AppDbContext context, BaseLogger<Usuario> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Usuario>(context, logger, httpContext)
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios() => await GetAll<UsuarioDto>();
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id) => await GetById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, UsuarioRequest usuarioRequest) => 
            await Update(id, ConvertTypes(usuarioRequest));

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioRequest usuarioRequest) {
            var erros = ValidarCadastro(usuarioRequest);

            if (erros.Count > 0) return BadRequest(new { Erros = erros });

            usuarioRequest.Senha = GerarHashSenha(usuarioRequest.Senha);

            var usuario = ConvertTypes(usuarioRequest);
            usuario.HashSenha = usuarioRequest.Senha;

			return await Create(usuario);
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Usuarios.Any(e => e.Id == id);
        protected override bool EntityExistsByEmail(string email) => _context.Usuarios.Any(e => e.Email == email);
        protected override bool EntityExistsByApelido(string apelido) => _context.Usuarios.Any(e => e.Apelido == apelido);

        private List<string> ValidarCadastro(UsuarioRequest usuario)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(usuario.Nome) || usuario.Nome.Length < 3)
            {
                erros.Add("Nome deve conter no mínimo 3 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Apelido) || usuario.Apelido.Length < 2)
            {
                erros.Add("Apelido deve conter no mínimo 2 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Email) ||
                !System.Text.RegularExpressions.Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                erros.Add("Email inválido.");
            }

            if (EntityExistsByEmail(usuario.Email)) erros.Add("Email ja cadastrado");

            var senha = usuario.Senha ?? "";
            if (senha.Length < 8 ||
                !senha.Any(char.IsLetter) ||
                !senha.Any(char.IsDigit) ||
                !senha.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                erros.Add("Senha deve ter no mínimo 8 caracteres, com letras, números e um caractere especial.");
            }

            if (usuario.DataNascimento == default)
            {
                erros.Add("Data de nascimento é obrigatória.");
            }

            if (EntityExistsByApelido(usuario.Apelido)) erros.Add("Ja existe um jogador " + usuario.Apelido);

            return erros;
        }

        private string GerarHashSenha(string senha)
        {
            using var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(senha, salt, 100_000);
            byte[] hash = pbkdf2.GetBytes(32);

            byte[] hashBytes = new byte[48];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            return Convert.ToBase64String(hashBytes);
        }

        
    }
}
