using fiapcloudgames.usuario.Application.Services.Interfaces;
using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioEmail;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioNome;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioSobrenome;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioNome;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fiapcloudgames.usuario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Autorização JWT para todas as rotas
public class UsuarioAggregateController : ControllerBase
{
	private readonly IUsuarioService _usuarioService;

	public UsuarioAggregateController(IUsuarioService usuarioService)
	{
		_usuarioService = usuarioService;		
	}

	[HttpPost]
	//[SwaggerOperation("Criar novo usuário")]
	[AllowAnonymous] // Permitir cadastro sem autenticação
	[ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CriarUsuario([FromBody]CreateUsuarioCommand command)
	{
		await _usuarioService.CriarUsuarioAsync(command);	
		return Ok();
	}

	[HttpPut()]
	//[SwaggerOperation("Atualizar email do usuário por ID")]
	public async Task<IActionResult> AtualizarEmail([FromBody] UpdateUsuarioEmailCommand command)
	{			
		await _usuarioService.AlterarEmailAsync(command);
		return Ok();
	}

	[HttpPut("/nome")]
	//[SwaggerOperation("Atualizar nome do usuário por ID")]
	public async Task<IActionResult> AtualizarNome([FromBody] UpdateUsuarioNomeCommand command)
	{
		await _usuarioService.AlterarNomeAsync(command);
		return Ok();
	}

	[HttpPut("/sobrenome")]
	//[SwaggerOperation("Atualizar sobrenome do usuário por ID")]
	public async Task<IActionResult> AtualizarSobrenome([FromBody] UpdateUsuarioSobrenomeCommand command)
	{
		await _usuarioService.AlterarSobrenomeAsync(command);
		return Ok();
	}

	[HttpGet("{id}")]
	//[SwaggerOperation("Buscar usuário por ID do agregado")]
	public async Task<IActionResult> ObterUsuario(string id)
	{
		var usuario =  await _usuarioService.GetByIdAsync(id);
		return Ok(usuario);
	}

	[HttpGet("{id}/events")]
	public async Task<IActionResult> ObterEventos(string id)
	{
		var eventList = await _usuarioService.GetEventsAsync(id);
		return Ok(eventList);
	}

}