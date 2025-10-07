using FiapCloudGamesAPI.EventSourcing.Agregados;
using FiapCloudGamesAPI.EventStore.API.Write;
using FiapCloudGamesAPI.EventStore.Infra;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.EventStore.API.Handlers
{
	public class UsuarioAgreggateCommandHandler
	{
		private readonly IEventStore _eventStore;

		public UsuarioAgreggateCommandHandler(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		#region Commands
		public async Task Handle(AdicionarAvaliaçãoPeloUsuarioCommand command) { }
		public async Task Handle(AdicionarJogosAoUsuarioCommand command) { }
		public async Task Handle(AtualizarDadosUsuarioCommand command) { }
		public async Task Handle(AtualizarEmailUsuarioCommand command) 
		{
		}
		public async Task Handle(AtualizarNomeUsuarioCommand command) { }
		public async Task Handle(AtualizarSenhaUsuarioCommand command) { }
		public async Task Handle(AtualizarSobrenomeUsuarioCommand command) { }
		public async Task Handle(CriarUsuarioCommand command) 
		{
		}

		public void Handle(DesativarUsuarioCommand command) { }
		#endregion

	}
}
