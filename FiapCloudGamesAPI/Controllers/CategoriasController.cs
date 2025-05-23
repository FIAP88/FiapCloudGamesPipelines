using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Entidades.Dtos;
using FiapCloudGamesAPI.Entidades.Requests;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController(AppDbContext context, BaseLogger<Categoria> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Categoria>(context, logger, httpContext)
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetPermissoes() => await GetAll<CategoriaDto>();

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(long id) => await GetById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(long id, CategoriaRequest categoriaRequest) =>
            await Update(id, ConvertTypes(categoriaRequest));

        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(CategoriaRequest categoriaRequest) =>
            await Create(ConvertTypes(categoriaRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Categorias.Any(e => e.Id == id);

    }
}
