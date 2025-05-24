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
    [Authorize(Policy = "GerenciarPerfil")]
    public class PerfilsController(AppDbContext context, BaseLogger<Perfil> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Perfil>(context, logger, httpContext)
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilDto>>> GetPermissoes() => await GetAll<PerfilDto>();

        [HttpGet("{id}")]
        public async Task<ActionResult<Perfil>> GetPerfil(long id) => await GetById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfil(long id, PerfilRequest perfilRequest) =>
            await Update(id, ConvertTypes(perfilRequest));

        [HttpPost]
        public async Task<ActionResult<Perfil>> PostPerfil(PerfilRequest perfilRequest) =>
            await Create(ConvertTypes(perfilRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfil(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Perfis.Any(e => e.Id == id);
    }
}
