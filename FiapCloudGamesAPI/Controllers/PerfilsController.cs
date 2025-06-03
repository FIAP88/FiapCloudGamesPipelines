using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Entidades.Dtos;
using FiapCloudGamesAPI.Entidades.Requests;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "GerenciarPerfil")]
    [SwaggerTag("Gerenciamento de Perfis")]
    public class PerfilsController(AppDbContext context, BaseLogger<Perfil> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Perfil>(context, logger, httpContext)
    {

        [HttpGet]
        [SwaggerOperation("Buscar todos os perfis")]
        public async Task<ActionResult<IEnumerable<PerfilDto>>> GetPermissoes() => await GetAll<PerfilDto>();

        [HttpGet("{id}")]
        [SwaggerOperation("Buscar perfil por ID")]
        public async Task<ActionResult<Perfil>> GetPerfil(long id) => await GetById(id);

        [HttpPut("{id}")]
        [SwaggerOperation("Atualizar perfil por ID")]
        public async Task<IActionResult> PutPerfil(long id, PerfilRequest perfilRequest) =>
            await Update(id, ConvertTypes(perfilRequest));

        [HttpPost]
        [SwaggerOperation("Criar novo perfil")]
        public async Task<ActionResult<Perfil>> PostPerfil(PerfilRequest perfilRequest) =>
            await Create(ConvertTypes(perfilRequest));

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletar perfil por ID")]
        public async Task<IActionResult> DeletePerfil(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Perfis.Any(e => e.Id == id);
    }
}
