using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecasDoJogadoresController(AppDbContext context, BaseLogger<BibliotecaDoJogador> logger) :
        BaseControllerFiapCloudGames<BibliotecaDoJogador>(context, logger)
    {

        // GET: api/BibliotecasDoJogadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BibliotecaDoJogador>>> GetBibliotecasDoJogador()
        {
            return await _context.BibliotecasDoJogador.ToListAsync();
        }

        // GET: api/BibliotecasDoJogadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BibliotecaDoJogador>> GetBibliotecaDoJogador(int id)
        {
            var bibliotecaDoJogador = await _context.BibliotecasDoJogador.FindAsync(id);

            if (bibliotecaDoJogador == null)
            {
                return NotFound();
            }

            return bibliotecaDoJogador;
        }

        // PUT: api/BibliotecasDoJogadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBibliotecaDoJogador(int id, BibliotecaDoJogador bibliotecaDoJogador)
        {
            if (id != bibliotecaDoJogador.Id)
            {
                return BadRequest();
            }

            _context.Entry(bibliotecaDoJogador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BibliotecaDoJogadorExists(id))
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

        // POST: api/BibliotecasDoJogadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BibliotecaDoJogador>> PostBibliotecaDoJogador(BibliotecaDoJogador bibliotecaDoJogador)
        {
            _context.BibliotecasDoJogador.Add(bibliotecaDoJogador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBibliotecaDoJogador", new { id = bibliotecaDoJogador.Id }, bibliotecaDoJogador);
        }

        // DELETE: api/BibliotecasDoJogadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBibliotecaDoJogador(int id)
        {
            var bibliotecaDoJogador = await _context.BibliotecasDoJogador.FindAsync(id);
            if (bibliotecaDoJogador == null)
            {
                return NotFound();
            }

            _context.BibliotecasDoJogador.Remove(bibliotecaDoJogador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BibliotecaDoJogadorExists(int id)
        {
            return _context.BibliotecasDoJogador.Any(e => e.Id == id);
        }
    }
}
