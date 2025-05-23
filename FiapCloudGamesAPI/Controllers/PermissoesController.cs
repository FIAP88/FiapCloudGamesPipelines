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
    public class PermissoesController(AppDbContext context, BaseLogger<Permissao> logger, IHttpContextAccessor httpContext) :
        BaseControllerCrud<Permissao>(context, logger, httpContext)
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissaoDto>>> GetPermissoes() => await GetAll<PermissaoDto>();

        [HttpGet("{id}")]
        public async Task<ActionResult<Permissao>> GetPermissao(long id) => await GetById(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermissao(long id, PermissaoRequest permissaoRequest) =>
            await Update(id, ConvertTypes(permissaoRequest));

        [HttpPost]
        public async Task<ActionResult<Permissao>> PostPermissao(PermissaoRequest permissaoRequest) =>
            await Create(ConvertTypes(permissaoRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissao(long id) => await Delete(id);

        protected override bool EntityExists(long id) => _context.Permissoes.Any(e => e.Id == id);
    }
}
