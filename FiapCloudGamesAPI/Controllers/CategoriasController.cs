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
    public class CategoriasController(AppDbContext context, BaseLogger<Categoria> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Categoria>(context, logger, httpContext)
    {
        [HttpGet]
        [Authorize(Policy = "BuscarCategorias")]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetPermissoes() => await GetAll<CategoriaDto>();

        [HttpGet("{id}")]
        [Authorize(Policy = "BuscarCategoriaPorId")]
        public async Task<ActionResult<Categoria>> GetCategoria(long id) => await GetById(id);

        [HttpPut("{id}")]
        [Authorize(Policy = "AtualizarCategoria")]
        public async Task<IActionResult> PutCategoria(long id, CategoriaRequest categoriaRequest) =>
            await Update(id, ConvertTypes(categoriaRequest));

        [HttpPost]
        [Authorize(Policy = "CriarCategoria")]
        public async Task<ActionResult<Categoria>> PostCategoria(CategoriaRequest categoriaRequest) =>
            await Create(ConvertTypes(categoriaRequest));

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeletarCategoria")]
        public async Task<IActionResult> DeleteCategoria(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Categorias.Any(e => e.Id == id);

    }
}
