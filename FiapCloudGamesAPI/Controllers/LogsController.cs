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
using Swashbuckle.AspNetCore.Annotations;

namespace FiapCloudGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Gerenciamento de Logs")]
    public class LogsController(AppDbContext context, BaseLogger<Log> logger, IHttpContextAccessor httpContext) : 
        BaseControllerCrud<Log>(context, logger, httpContext)
    {
        // GET: api/Logs
        [HttpGet]
        [SwaggerOperation("Buscar todos os logs")]
        public async Task<ActionResult<IEnumerable<Log>>> GetLog() => await GetAll<Log>();

    }
}
