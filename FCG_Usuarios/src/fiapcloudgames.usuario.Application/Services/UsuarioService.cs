using fiapcloudgames.usuario.Application.DTOs;
using fiapcloudgames.usuario.Application.Services.Interfaces;
using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioEmail;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioNome;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioSobrenome;
using fiapcloudgames.usuario.Domain.Aggregates;
using fiapcloudgames.usuario.Domain.Events;
using fiapcloudgames.usuario.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlTypes;

namespace fiapcloudgames.usuario.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioAggregateRepository _repository;
		private readonly IEventDispatcher _eventDispatcher;

		public UsuarioService(IUsuarioAggregateRepository repository, IEventDispatcher eventDispatcher)
        {
			_repository = repository;
			_eventDispatcher = eventDispatcher;
        }

		public async Task CriarUsuarioAsync(CreateUsuarioCommand command)
		{
			// Cria o aggregate
			var aggregateId = Guid.NewGuid().ToString();
			var usuario = new UsuarioAggregate(
				aggregateId, 
				command.Nome, 
				command.Sobrenome, 
				command.Apelido, 
				command.Email, 
				command.Telefone, 
				command.HashSenha,
				command.DataNascimento, 
				command.PerfilId);

			// Eventos comitados e persistidos no EventStore
            var committedEvents = await _repository.SaveAsync(usuario);

            await _eventDispatcher.PublishAsync(committedEvents);
		}

        public async Task AlterarNomeAsync(UpdateUsuarioNomeCommand command)
        {
            // Recupera o aggregate do Event Store
            var usuario = await _repository.GetByIdAsync(command.UsuarioId.ToString());

            usuario.AlterarNome(command.NovoNome);

            var committedEvents = await _repository.SaveAsync(usuario);

            await _eventDispatcher.PublishAsync(committedEvents);
        }

        public async Task AlterarSobrenomeAsync(UpdateUsuarioSobrenomeCommand command)
        {
            // Recupera o aggregate do Event Store
            var usuario = await _repository.GetByIdAsync(command.UsuarioId.ToString());

            usuario.AlterarNome(command.NovoSobrenome);

            var committedEvents = await _repository.SaveAsync(usuario);

            await _eventDispatcher.PublishAsync(committedEvents);
        }

        public async Task AlterarEmailAsync(UpdateUsuarioEmailCommand command)
        {
            // Recupera o aggregate do Event Store
            var usuario = await _repository.GetByIdAsync(command.UsuarioId.ToString());

            usuario.AlterarEmail(command.NovoEmail);

            var committedEvents = await _repository.SaveAsync(usuario);

            await _eventDispatcher.PublishAsync(committedEvents);
        }

        public async Task<UsuarioAggregateDto> GetByIdAsync(string aggregateId)
        {
			var usuarioAggregate = await _repository.GetByIdAsync(aggregateId);

			return new UsuarioAggregateDto
			{
				Id = usuarioAggregate.Id,
				Nome = usuarioAggregate.Nome,
				Sobrenome = usuarioAggregate.Sobrenome,
				Email = usuarioAggregate.Email,
				DataNascimento = usuarioAggregate.DataNascimento,
				Version = usuarioAggregate.Version
			};
		}

        public async Task<List<UsuarioAggregateHistoryDto>> GetEventsAsync(string aggregateId)
        {
			var events = await _repository.GetEventsAsync(aggregateId);

			//if (!events.Any())
			//	return NotFound();

			var eventList = events.Select(e => new UsuarioAggregateHistoryDto
			{
				//Id = e.Id,
				//AggregateId = e.AggregateId,
				EventType = e.EventType,
				Version = e.Version,
				Timestamp = e.Timestamp,
				EventData = JsonConvert.SerializeObject(e, Formatting.None)
			});
            
            return eventList.ToList();			

		}
	}
}
