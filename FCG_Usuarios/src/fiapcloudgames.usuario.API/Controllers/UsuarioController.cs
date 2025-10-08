using fiapcloudgames.usuario.Application.Services.Interfaces;
using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace fiapcloudgames.usuario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Autorização JWT para todas as rotas
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>Cadastra um novo usuário.</summary>
        [HttpPost]
        [AllowAnonymous] // Permitir cadastro sem autenticação
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateUsuarioCommand command)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var result = _usuarioService.Cadastrar(command);
                if (result is null)
                    return Problem(title: "Falha ao cadastrar usuário", statusCode: (int)HttpStatusCode.BadRequest);

                var idProp = result?.GetType().GetProperty("Id");
                var idVal = idProp?.GetValue(result);

                if (idVal is null)
                    return Created("api/usuarios", result);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = idVal },
                    result
                );
            }
            catch (ArgumentException ex)
            {
                return Problem(title: "Erro de validação", detail: ex.Message, statusCode: (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Problem(title: "Erro ao criar usuário", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>Obtém um usuário pelo Id.</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var usuario = _usuarioService.ObterPorId(id);
            if (usuario is null) return NotFound();
            return Ok(usuario);
        }

        /// <summary>Lista usuários.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _usuarioService.ObterTodos();
            return Ok(result);
        }

        /// <summary>Atualiza um usuário pelo Id.</summary>
        [HttpPut]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] UpdateUsuarioCommand command)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var updated = _usuarioService.Alterar(command, "TODO:usuario");
                if (updated is null) return NotFound();
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return Problem(title: "Erro de validação", detail: ex.Message, statusCode: (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Problem(title: "Erro ao atualizar usuário", detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>Exclui um usuário pelo Id.</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var removed = _usuarioService.Deletar(id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}