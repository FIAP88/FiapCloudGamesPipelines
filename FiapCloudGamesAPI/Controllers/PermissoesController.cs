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
    [Authorize(Policy = "GerenciarPermissoes")]
    [SwaggerTag("Gerenciamento de Permissões")]
    public class PermissoesController(AppDbContext context, BaseLogger<Permissao> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Permissao>(context, logger, httpContext)
    {

        [HttpGet]
        [SwaggerOperation("Buscar todas as permissões")]
        public async Task<ActionResult<IEnumerable<PermissaoDto>>> GetPermissoes() => await GetAll<PermissaoDto>();

        [HttpGet("{id}")]
        [SwaggerOperation("Buscar permissão por ID")]
        public async Task<ActionResult<Permissao>> GetPermissao(long id) => await GetById(id);

        [HttpPut("{id}")]
        [SwaggerOperation("Atualizar permissão por ID")]
        public async Task<IActionResult> PutPermissao(long id, PermissaoRequest permissaoRequest) =>
            await Update(id, ConvertTypes(permissaoRequest));

        [HttpPost]
        [SwaggerOperation("Criar nova permissão")]
        public async Task<ActionResult<Permissao>> PostPermissao(PermissaoRequest permissaoRequest) =>
            await Create(ConvertTypes(permissaoRequest));

        [HttpDelete("{id}")]
        [SwaggerOperation("Deletar permissão por ID")]
        public async Task<IActionResult> DeletePermissao(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Permissoes.Any(e => e.Id == id);
    }
}
