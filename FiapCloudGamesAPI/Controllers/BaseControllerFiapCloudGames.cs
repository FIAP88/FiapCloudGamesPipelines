using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGamesAPI.Controllers
{
    public class BaseControllerFiapCloudGames<T> : ControllerBase where T : class
    {
        protected readonly AppDbContext _context;
        private readonly BaseLogger<T> _logger;

        public BaseControllerFiapCloudGames(AppDbContext context, BaseLogger<T> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            try
            {
                var result = await _context.Set<T>().ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAll: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        public virtual async Task<ActionResult<T>> GetById(long id)
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
    }
}
