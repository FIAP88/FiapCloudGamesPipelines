using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGamesAPI.Controllers
{
    public class BaseControllerFiapCloudGames<T> : ControllerBase where T : class
    {
        protected readonly AppDbContext _context;
        private readonly BaseLogger<T> _logger;
        protected Usuario? _usuario;
        
        public BaseControllerFiapCloudGames(AppDbContext context, BaseLogger<T> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _usuario = httpContextAccessor?.HttpContext?.Items["Usuario"] as Usuario;
        }

        protected string NomeUsuarioLogado { get => _usuario?.Nome ?? string.Empty; }

        protected async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            try
            {
                //_logger.LogInformation("teste");
                var result = await _context.Set<T>().AsNoTracking().ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAll: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        protected async Task<ActionResult<T>> GetById(long id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetById: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        protected async Task<IActionResult> Update(long id, T entity)
        {
            _logger.LogInformation($"Updating entity with ID: {id}");
            if (id != (entity as dynamic).Id)
            {
                return BadRequest();
            }
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Entity with ID: {id} updated successfully");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        protected async Task<ActionResult<T>> Create(T entity)
        {
            try
            {
                _logger.LogInformation($"Creating entity: {entity}");
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Entity created successfully: {entity}");
                return CreatedAtAction("GetById", new { id = (entity as dynamic).Id }, entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Create: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        protected async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation($"Deleting entity with ID: {id}");
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Entity with ID: {id} deleted successfully");

            return NoContent();
        }

        protected virtual bool EntityExists(long id)
        {
            throw new NotImplementedException();
        }

        protected virtual bool EntityExistsByEmail(string email)
        {
            throw new NotImplementedException();
        }

        protected virtual bool EntityExistsByApelido(string apelido)
        {
            throw new NotImplementedException();
        }
    }
}
