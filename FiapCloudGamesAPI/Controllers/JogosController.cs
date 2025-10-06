using FCG_API_Jogos.Services;
using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Entidades.Dtos;
using FiapCloudGamesAPI.Entidades.Requests;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Gerenciamento de Jogos")]
    public class JogosController(
        AppDbContext context, 
        BaseLogger<Jogo> logger, 
        IHttpContextAccessor httpContext,
        IJogoElasticService elasticService) :
        BaseControllerCrud<Jogo>(context, logger, httpContext)
    {

        private readonly IJogoElasticService _elasticService = elasticService;

        [HttpGet]
        [Authorize(Policy = "BuscarJogos")]
        [SwaggerOperation("Buscar todos os jogos")]
        public async Task<ActionResult<IEnumerable<JogoDto>>> GetPermissoes() => await GetAll<JogoDto>();

        [HttpGet("Buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string termo)
        {
            var resultados = await _elasticService.BuscarAsync(termo);
            return Ok(resultados);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "BuscarJogoPorId")]
        [SwaggerOperation("Buscar jogo por ID")]
        public async Task<ActionResult<Jogo>> GetJogo(long id) => await GetById(id);

        [HttpPut("{id}")]
        [Authorize(Policy = "AtualizarJogo")]
        [SwaggerOperation("Atualizar jogo por ID")]
        public async Task<IActionResult> PutJogo(long id, JogoRequest jogoRequest)
        {
            Jogo atualizaJogo = ConvertTypes(jogoRequest);
            var result = await Update(id, atualizaJogo);
            await _elasticService.AtualizarAsync(atualizaJogo);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "CriarJogos")]
        [SwaggerOperation("Criar novo jogo")]
        public async Task<ActionResult<Jogo>> PostJogo(JogoRequest jogoRequest)
        {
            Jogo novoJogo = ConvertTypes(jogoRequest);
            var result = await Create(novoJogo);
            await _elasticService.IndexarAsync(novoJogo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeletarJogo")]
        [SwaggerOperation("Deletar jogo por ID")]
        public async Task<IActionResult> DeleteJogo(long id)
        {
            var result = await Delete(id);
            await _elasticService.RemoverAsync(id.ToString());
            return Ok(result);
        }

        [HttpGet("MeusJogos")]
        [Authorize(Policy = "MeusJogos")]
        [SwaggerOperation("Buscar meus jogos")]
        public async Task<ActionResult<List<Jogo>>> GetMeusJogos()
        {
            var jogos = await _context.JogosUsuarios.IgnoreAutoIncludes().Include(ju => ju.Jogo)
                .Where(u => u.UsuarioId == IdUsuarioLogado)
                .Select(ju => ConvertTypes<JogoDto>(ju.Jogo))
            .ToListAsync();
            return Ok(jogos);
        }

        [HttpPost("MeusJogos/AdicionarJogo")]
        [Authorize(Policy = "AdicionarJogo")]
        [SwaggerOperation("Adicionar jogo à minha lista de jogos")]
        public async Task<IActionResult> AdicionarJogo(long idJogo)
        {
            if(await _context.JogosUsuarios.AsNoTracking().CountAsync(j => j.UsuarioId == IdUsuarioLogado && j.JogoId == idJogo ) != 0)
                return BadRequest("Jogo já adicionado à sua lista de jogos.");
            else if(await _context.Jogos.AsNoTracking().FirstOrDefaultAsync(j => j.Id == idJogo) == null)
                return BadRequest("Jogo não cadastrado no sistema.");

            await _context.JogosUsuarios.AddAsync(new JogoUsuario { UsuarioId = IdUsuarioLogado, JogoId = idJogo });
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Jogo com ID: {idJogo} adicionado à lista de jogos do usuário com ID: {IdUsuarioLogado}.");
            return Ok();
        }

        [HttpPost("Sugerir")]
        public async Task<IActionResult> Sugerir([FromBody] IEnumerable<string> historicoTags)
        {
            var sugestoes = await _elasticService.SugerirBaseadoNoHistoricoAsync(historicoTags);
            return Ok(sugestoes);
        }

        [HttpGet("Populares")]
        public async Task<IActionResult> MaisPopulares([FromQuery] int top = 5)
        {
            var metricas = await _elasticService.ObterJogosMaisPopularesAsync(top);
            return Ok(metricas);
        }


        protected override bool EntityExists(long id) => _context.Jogos.Any(e => e.Id == id);
    }
}
