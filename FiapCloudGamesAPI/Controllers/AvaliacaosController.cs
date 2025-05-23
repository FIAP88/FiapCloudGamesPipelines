using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Entidades.Dtos;
using FiapCloudGamesAPI.Entidades.Requests;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaosController(AppDbContext context, BaseLogger<Avaliacao> logger, IHttpContextAccessor httpContext) : 
        BaseControllerCrud<Avaliacao>(context, logger, httpContext)
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvaliacaoDto>>> GetPermissoes() => await GetAll<AvaliacaoDto>();

        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliacao>> GetAvaliacao(long id) => await GetById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvaliacao(long id, AvaliacaoRequest avaliacaoRequest) =>
            await Update(id, ConvertTypes(avaliacaoRequest));

        [HttpPost]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(AvaliacaoRequest avaliacaoRequest) =>
            await Create(ConvertTypes(avaliacaoRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvaliacao(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Avaliacoes.Any(e => e.Id == id);
    }
}
