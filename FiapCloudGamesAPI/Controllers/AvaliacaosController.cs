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
    public class AvaliacaosController(AppDbContext context, BaseLogger<Avaliacao> logger, IHttpContextAccessor httpContext) : 
        BaseControllerCrud<Avaliacao>(context, logger, httpContext)
    {
        [HttpGet]
        [Authorize(Policy = "BuscarAvaliacoes")]
        public async Task<ActionResult<IEnumerable<AvaliacaoDto>>> GetPermissoes() => await GetAll<AvaliacaoDto>();

        [HttpGet("{id}")]
        [Authorize(Policy = "BuscarAvaliacaoPorId")]
        public async Task<ActionResult<Avaliacao>> GetAvaliacao(long id) => await GetById(id);

        [HttpPut("{id}")]
        [Authorize(Policy = "AtualizarAvaliacao")]
        public async Task<IActionResult> PutAvaliacao(long id, AvaliacaoRequest avaliacaoRequest) =>
            await Update(id, ConvertTypes(avaliacaoRequest));

        [HttpPost]
        [Authorize(Policy = "CriarAvaliacao")]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(AvaliacaoRequest avaliacaoRequest) =>
            await Create(ConvertTypes(avaliacaoRequest));

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeletarAvaliacao")]
        public async Task<IActionResult> DeleteAvaliacao(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Avaliacoes.Any(e => e.Id == id);
    }
}
