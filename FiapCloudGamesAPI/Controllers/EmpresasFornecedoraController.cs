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

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasFornecedoraController(AppDbContext context, BaseLogger<EmpresaFornecedora> logger, IHttpContextAccessor httpContext) :
        BaseControllerFiapCloudGames<EmpresaFornecedora>(context, logger, httpContext)
    {

        // GET: api/EmpresasFornecedora
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaFornecedora>>> GetEmpresasFornecedoras()
        {
            return await _context.EmpresasFornecedoras.ToListAsync();
        }

        // GET: api/EmpresasFornecedora/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaFornecedora>> GetEmpresaFornecedora(int id)
        {
            var empresaFornecedora = await _context.EmpresasFornecedoras.FindAsync(id);

            if (empresaFornecedora == null)
            {
                return NotFound();
            }

            return empresaFornecedora;
        }

        // PUT: api/EmpresasFornecedora/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresaFornecedora(int id, EmpresaFornecedora empresaFornecedora)
        {
            if (id != empresaFornecedora.Id)
            {
                return BadRequest();
            }

            _context.Entry(empresaFornecedora).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaFornecedoraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmpresasFornecedora
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpresaFornecedora>> PostEmpresaFornecedora(EmpresaFornecedora empresaFornecedora)
        {
            _context.EmpresasFornecedoras.Add(empresaFornecedora);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmpresaFornecedoraExists(empresaFornecedora.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmpresaFornecedora", new { id = empresaFornecedora.Id }, empresaFornecedora);
        }

        // DELETE: api/EmpresasFornecedora/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresaFornecedora(int id)
        {
            var empresaFornecedora = await _context.EmpresasFornecedoras.FindAsync(id);
            if (empresaFornecedora == null)
            {
                return NotFound();
            }

            _context.EmpresasFornecedoras.Remove(empresaFornecedora);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpresaFornecedoraExists(int id)
        {
            return _context.EmpresasFornecedoras.Any(e => e.Id == id);
        }
    }
}
