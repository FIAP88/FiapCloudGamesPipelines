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
    [SwaggerTag("Gerenciamento de Avaliações")]
    public class AvaliacaosController(AppDbContext context, BaseLogger<Avaliacao> logger, IHttpContextAccessor httpContext) : 
        BaseControllerCrud<Avaliacao>(context, logger, httpContext)
    {
        [HttpGet]
        [Authorize(Policy = "BuscarAvaliacoes")]
        [SwaggerOperation("Buscar todas as avaliações")]
        public async Task<ActionResult<IEnumerable<AvaliacaoDto>>> GetPermissoes() => await GetAll<AvaliacaoDto>();

        [HttpGet("{id}")]
        [Authorize(Policy = "BuscarAvaliacaoPorId")]
        [SwaggerOperation("Buscar avaliação por ID")]
        public async Task<ActionResult<Avaliacao>> GetAvaliacao(long id) => await GetById(id);

        [HttpPut("{id}")]
        [Authorize(Policy = "AtualizarAvaliacao")]
        [SwaggerOperation("Atualizar avaliação por ID")]
        public async Task<IActionResult> PutAvaliacao(long id, AvaliacaoRequest avaliacaoRequest) =>
            await Update(id, ConvertTypes(avaliacaoRequest));

        [HttpPost]
        [Authorize(Policy = "CriarAvaliacao")]
        [SwaggerOperation("Criar nova avaliação")]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(AvaliacaoRequest avaliacaoRequest) =>
            await Create(ConvertTypes(avaliacaoRequest));

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeletarAvaliacao")]
        [SwaggerOperation("Deletar avaliação por ID")]
        public async Task<IActionResult> DeleteAvaliacao(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Avaliacoes.Any(e => e.Id == id);
    }
}
