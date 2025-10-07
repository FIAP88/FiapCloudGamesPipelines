using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario
{
	public record UsuarioSenhaAlterada : DomainEvent
	{
		public string? NovoHashSenha { get; set; }
		public UsuarioSenhaAlterada()
		{
			EventType = nameof(UsuarioSenhaAlterada);
		}
	}
}