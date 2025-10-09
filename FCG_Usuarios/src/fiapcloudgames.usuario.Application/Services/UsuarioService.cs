using fiapcloudgames.usuario.Application.Services.Interfaces;
using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioEmail;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioNome;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioSobrenome;
using fiapcloudgames.usuario.Domain.Aggregates;
using fiapcloudgames.usuario.Domain.Interfaces;

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
			var aggregateId = new Guid().ToString();
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

    }
}
