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
    public class JogosController(AppDbContext context, BaseLogger<Jogo> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Jogo>(context, logger, httpContext)
    {

        [HttpGet]
        [Authorize(Policy = "BuscarJogos")]
        public async Task<ActionResult<IEnumerable<JogoDto>>> GetPermissoes() => await GetAll<JogoDto>();

        [HttpGet("{id}")]
        [Authorize(Policy = "BuscarJogoPorId")]
        public async Task<ActionResult<Jogo>> GetJogo(long id) => await GetById(id);

        [HttpPut("{id}")]
        [Authorize(Policy = "AtualizarJogo")]
        public async Task<IActionResult> PutJogo(long id, JogoRequest jogoRequest) =>
            await Update(id, ConvertTypes(jogoRequest));

        [HttpPost]
        [Authorize(Policy = "CriarJogos")]
        public async Task<ActionResult<Jogo>> PostJogo(JogoRequest jogoRequest) =>
            await Create(ConvertTypes(jogoRequest));

        [HttpDelete("{id}")]
        [Authorize(Roles = "DeletarJogo")]
        public async Task<IActionResult> DeleteJogo(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Jogos.Any(e => e.Id == id);
    }
}
