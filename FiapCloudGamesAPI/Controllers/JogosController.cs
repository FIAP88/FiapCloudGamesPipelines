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
    public class JogosController(AppDbContext context, BaseLogger<Jogo> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Jogo>(context, logger, httpContext)
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoDto>>> GetPermissoes() => await GetAll<JogoDto>();

        [HttpGet("{id}")]
        public async Task<ActionResult<Jogo>> GetJogo(long id) => await GetById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJogo(long id, JogoRequest jogoRequest) =>
            await Update(id, ConvertTypes(jogoRequest));

        [HttpPost]
        public async Task<ActionResult<Jogo>> PostJogo(JogoRequest jogoRequest) =>
            await Create(ConvertTypes(jogoRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogo(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Jogos.Any(e => e.Id == id);
    }
}
