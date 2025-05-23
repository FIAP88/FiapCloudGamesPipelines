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
    [Authorize]
    public class UsuariosController(AppDbContext context, BaseLogger<Usuario> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Usuario>(context, logger, httpContext)
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios() => await GetAll<UsuarioDto>();
        
        [HttpGet("{id}")]
        [Authorize(Roles = "4")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id) => await GetById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, UsuarioRequest usuarioRequest) => 
            await Update(id, ConvertTypes(usuarioRequest));

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioRequest usuarioRequest) =>
            await Create(ConvertTypes(usuarioRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Usuarios.Any(e => e.Id == id);
    }
}
