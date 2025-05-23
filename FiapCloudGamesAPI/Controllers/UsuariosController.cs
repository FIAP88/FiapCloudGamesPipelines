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
            var usuario = ObterUsuarioByUsuarioRequest(usuarioRequest);
            return await Create(usuario);
        } 

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Usuarios.Any(e => e.Id == id);

        private Usuario ObterUsuarioByUsuarioRequest(UsuarioRequest usuarioRequest)
        {
            return new Usuario(usuarioRequest.Nome, usuarioRequest.Sobrenome, usuarioRequest.Apelido, usuarioRequest.Email,
                usuarioRequest.Senha, usuarioRequest.DataNascimento, usuarioRequest.PerfilId, NomeUsuarioLogado)
            {
                
                Nome = usuarioRequest.Nome,
            };
        }
    }
}
