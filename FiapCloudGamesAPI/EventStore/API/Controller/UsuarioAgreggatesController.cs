using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.EventSourcing.Agregados;
using FiapCloudGamesAPI.EventStore.API.Write;
using FiapCloudGamesAPI.EventStore.Domain.Dto;
using FiapCloudGamesAPI.EventStore.Infra;
using FiapCloudGamesAPI.EventStore.Infraestructure;
using FiapCloudGamesAPI.EventStore.Projection.Projector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "GerenciarUsuarios")]
[SwaggerTag("EventSourcing")]
public class UsuarioAggregateController : ControllerBase
{
	private readonly IUsuarioAggregateRepository _repository;
	private readonly UsuarioAggregateReadModelProjector _projector;

	public UsuarioAggregateController(IUsuarioAggregateRepository repository, UsuarioAggregateReadModelProjector projector)
	{
		_repository = repository;		
		_projector = projector;
	}

	[HttpPost]
	[SwaggerOperation("Criar novo usuário")]
	public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioCommand command)
	{
		var usuarioAgreggateId = Guid.NewGuid().ToString();

		var usuarioAggregate = new UsuarioAggregate(
			usuarioAgreggateId, 
			command.Nome, 
			command.Sobrenome,
			command.Apelido,
			command.Email,
			command.HashSenha,
			command.DataNascimento,
			command.PerfilId			
			);

		// Eventos nao comitados
		var eventos = usuarioAggregate.GetUncommittedEvents().ToList();

		await _repository.SaveAsync(usuarioAggregate);

		// Atualiza a Projeção

		foreach (var evt in eventos)
		{
			await _projector.Handle(evt);
		}
		// 

		return Ok(new { usuarioAggregate.Id });

	}

	[HttpPut("{id}/email")]
	[SwaggerOperation("Atualizar email do usuário por ID")]
	public async Task<IActionResult> AtualizarEmail(string id, [FromBody] AtualizarEmailUsuarioCommand command)
	{			
		var usuarioAggregate = await _repository.GetByIdAsync(id);

		if (usuarioAggregate == null)
			return NotFound();

		usuarioAggregate.AlterarEmail(command.NovoEmail);

        // Eventos nao comitados
        var eventos = usuarioAggregate.GetUncommittedEvents().ToList();

		await _repository.SaveAsync(usuarioAggregate);
        
		// Atualiza a Projeção
		foreach (var evt in eventos)
		{
			await _projector.Handle(evt);
		}
		// 

		return Ok();
	}

	[HttpPut("{id}/nome")]
	[SwaggerOperation("Atualizar nome do usuário por ID")]
	public async Task<IActionResult> AtualizarNome(string id, [FromBody] AtualizarNomeUsuarioCommand command)
	{
		var usuarioAggregate = await _repository.GetByIdAsync(id);
		if (usuarioAggregate == null)
			return NotFound();

		usuarioAggregate.AlterarNome(command.NovoNome);
		await _repository.SaveAsync(usuarioAggregate);
		return Ok();
	}

	[HttpPut("{id}/sobrenome")]
	[SwaggerOperation("Atualizar sobrenome do usuário por ID")]
	public async Task<IActionResult> AtualizarSobrenome(string id, [FromBody] AtualizarSobrenomeUsuarioCommand command)
	{
		var usuarioAggregate = await _repository.GetByIdAsync(id);
		if (usuarioAggregate == null)
			return NotFound();

		usuarioAggregate.AlterarSobrenome(command.NovoSobrenome);

		// Atualiza a Projeção
		var eventos = usuarioAggregate.GetUncommittedEvents().ToList();
		
		await _repository.SaveAsync(usuarioAggregate);

		foreach (var evt in eventos)
		{
			await _projector.Handle(evt);
		}
		//

		return Ok();
	}

	[HttpPut("{id}/senha")]
	[SwaggerOperation("Atualizar senha do usuário por ID")]
	public async Task<IActionResult> AtualizarSenha(string id, [FromBody] AtualizarSenhaUsuarioCommand command)
	{
		var usuarioAggregate = await _repository.GetByIdAsync(id);
		if (usuarioAggregate == null)
			return NotFound();

		usuarioAggregate.AlterarSenha(command.NovoHashSenha);
		await _repository.SaveAsync(usuarioAggregate);
		return Ok();
	}

	[HttpGet("{id}")]
	[SwaggerOperation("Buscar usuário por ID do agregado")]
	public async Task<IActionResult> ObterUsuario(string id)
	{
		var usuarioAggregate = await _repository.GetByIdAsync(id);
		if (usuarioAggregate == null)
			return NotFound();

		return Ok(new UsuarioAggregateDto { 
			Id = usuarioAggregate.Id,
			Nome = usuarioAggregate.Nome,
			Sobrenome = usuarioAggregate.Sobrenome,	
			Email = usuarioAggregate.Email,
			DataNascimento = usuarioAggregate.DataNascimento,
			Version = usuarioAggregate.Version});

	}

	[HttpGet("{id}/events")]
	public async Task<IActionResult> ObterEventos(string id)
	{
		var events = await _repository.GetEventsAsync(id);

		if (!events.Any())
			return NotFound();

		var eventList = events.Select(e => new UsuarioAggregateHistoryDto
		{
			//Id = e.Id,
			//AggregateId = e.AggregateId,
			EventType = e.EventType,
			Version = e.Version,
			Timestamp = e.Timestamp,
			EventData = JsonConvert.SerializeObject(e, Formatting.None)
		});

		return Ok(eventList);
	}

}