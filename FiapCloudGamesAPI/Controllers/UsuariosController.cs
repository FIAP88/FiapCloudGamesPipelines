using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Entidades.Requests;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController(AppDbContext context, BaseLogger<Usuario> logger, IHttpContextAccessor httpContext) :
        BaseControllerFiapCloudGames<Usuario>(context, logger, httpContext)
    {
        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios() => await GetAll();

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        [Authorize(Roles = "4")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id) => await GetById(id);

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, UsuarioRequest usuarioRequest)
        {
            var usuario = ObterUsuarioByUsuarioRequest(usuarioRequest);
            return await Update(id, usuario);
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioRequest usuarioRequest) {
            var erros = ValidarCadastro(usuarioRequest);

            if (erros.Count > 0) return BadRequest(new { Erros = erros });

            usuarioRequest.Senha = GerarHashSenha(usuarioRequest.Senha);
            usuarioRequest.PerfilId = 2;

            var usuario = ObterUsuarioByUsuarioRequest(usuarioRequest);

            return await Create(usuario);
        } 

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Usuarios.Any(e => e.Id == id);
        protected override bool EntityExistsByEmail(string email) => _context.Usuarios.Any(e => e.Email == email);
        protected override bool EntityExistsByApelido(string apelido) => _context.Usuarios.Any(e => e.Apelido == apelido);

        private Usuario ObterUsuarioByUsuarioRequest(UsuarioRequest usuarioRequest)
        {
            return new Usuario(usuarioRequest.Nome, usuarioRequest.Sobrenome, usuarioRequest.Apelido, usuarioRequest.Email,
                usuarioRequest.Senha, usuarioRequest.DataNascimento, usuarioRequest.PerfilId, NomeUsuarioLogado)
            {
                
                Nome = usuarioRequest.Nome,
            };
        }

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
