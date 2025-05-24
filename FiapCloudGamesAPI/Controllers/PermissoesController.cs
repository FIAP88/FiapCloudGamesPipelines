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
    [Authorize(Policy = "GerenciarPermissoes")]
    public class PermissoesController(AppDbContext context, BaseLogger<Permissao> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Permissao>(context, logger, httpContext)
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissaoDto>>> GetPermissoes() => await GetAll<PermissaoDto>();

        [HttpGet("{id}")]
        public async Task<ActionResult<Permissao>> GetPermissao(long id) => await GetById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermissao(long id, PermissaoRequest permissaoRequest) =>
            await Update(id, ConvertTypes(permissaoRequest));

        [HttpPost]
        public async Task<ActionResult<Permissao>> PostPermissao(PermissaoRequest permissaoRequest) =>
            await Create(ConvertTypes(permissaoRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissao(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Permissoes.Any(e => e.Id == id);
    }
}
