using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Entidades.Dtos;
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
    public class BibliotecasDoJogadoresController(AppDbContext context, BaseLogger<BibliotecaDoJogador> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<BibliotecaDoJogador>(context, logger, httpContext)
    {
        [HttpGet]
        [Authorize(Policy = "BuscarBibliotecasDoJogador")]
        public async Task<ActionResult<IEnumerable<BibliotecaDoJogadorDto>>> GetPermissoes() => await GetAll<BibliotecaDoJogadorDto>();

        [HttpGet("{id}")]
        [Authorize(Policy = "BuscarBibliotecaDoJogadorPorId")]
        public async Task<ActionResult<BibliotecaDoJogador>> GetBibliotecaDoJogador(long id) => await GetById(id);

        [HttpPut("{id}")]
        [Authorize(Policy = "AtualizarBibliotecaDoJogador")]
        public async Task<IActionResult> PutBibliotecaDoJogador(long id, BibliotecaDoJogadorRequest bibliotecaDoJogadorRequest) =>
            await Update(id, ConvertTypes(bibliotecaDoJogadorRequest));

        [HttpPost]
        [Authorize(Policy = "CriarBibliotecaDoJogador")]
        public async Task<ActionResult<BibliotecaDoJogador>> PostBibliotecaDoJogador(BibliotecaDoJogadorRequest bibliotecaDoJogadorRequest) =>
            await Create(ConvertTypes(bibliotecaDoJogadorRequest));

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeletarBibliotecaDoJogador")]
        public async Task<IActionResult> DeleteBibliotecaDoJogador(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.BibliotecasDoJogador.Any(e => e.Id == id);
    }
}
