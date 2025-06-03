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
    [SwaggerTag("Gerenciamento de Categorias")]
    public class CategoriasController(AppDbContext context, BaseLogger<Categoria> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Categoria>(context, logger, httpContext)
    {
        [HttpGet]
        [Authorize(Policy = "BuscarCategorias")]
        [SwaggerOperation("Buscar todas as categorias")]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetPermissoes() => await GetAll<CategoriaDto>();

        [HttpGet("{id}")]
        [Authorize(Policy = "BuscarCategoriaPorId")]
        [SwaggerOperation("Buscar categoria por ID")]
        public async Task<ActionResult<Categoria>> GetCategoria(long id) => await GetById(id);

        [HttpPut("{id}")]
        [Authorize(Policy = "AtualizarCategoria")]
        [SwaggerOperation("Atualizar categoria por ID")]
        public async Task<IActionResult> PutCategoria(long id, CategoriaRequest categoriaRequest) =>
            await Update(id, ConvertTypes(categoriaRequest));

        [HttpPost]
        [Authorize(Policy = "CriarCategoria")]
        [SwaggerOperation("Criar nova categoria")]
        public async Task<ActionResult<Categoria>> PostCategoria(CategoriaRequest categoriaRequest) =>
            await Create(ConvertTypes(categoriaRequest));

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeletarCategoria")]
        [SwaggerOperation("Deletar categoria por ID")]
        public async Task<IActionResult> DeleteCategoria(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Categorias.Any(e => e.Id == id);

    }
}
