using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Entidades;
using FiapCloudGamesAPI.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FiapCloudGamesAPI.Controllers
{
    public class BaseControllerCrud<T> : BaseController<T> where T : EntidadeBase
    {

        public BaseControllerCrud(AppDbContext context, BaseLogger<T> logger, IHttpContextAccessor httpContextAccessor) : 
            base(context, logger, httpContextAccessor)
        {

        }

        protected async Task<ActionResult<IEnumerable<T>>> GetAll() => await GetAll<T>();

        protected async Task<ActionResult<IEnumerable<S>>> GetAll<S>() where S : class
        {
            var result = await _context.Set<T>().AsNoTracking().ToListAsync();
            var castedResult = result.Select(ConvertTypes<S>).ToList();
            return Ok(castedResult);
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
            if (id != entity.Id)
            {
                return BadRequest();
            }
            entity.AtualizadoPor = NomeUsuarioLogado;
            entity.DataAtualizacao = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Entity with ID: {id} updated successfully");
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "Erro interno no servidor");
                }
            }
        }

        protected async Task<ActionResult<T>> Create(T entity)
        {
            _logger.LogInformation($"Creating entity: {entity}");
            entity.CriadoPor = NomeUsuarioLogado;
            entity.DataCriacao = DateTime.Now;
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Entity created successfully: {entity}");
            return await GetById(entity.Id);
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

        protected static S ConvertTypes<S>(T obj)
        {
            return JsonConvert.DeserializeObject<S>(JsonConvert.SerializeObject(obj));
        }

        protected static T ConvertTypes<S>(S obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
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
