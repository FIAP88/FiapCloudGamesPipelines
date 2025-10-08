using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario
{
	public record UsuarioNomeAlterado : DomainEvent
	{
		public string? NovoNome { get; set; }
		public UsuarioNomeAlterado()
		{
			EventType = nameof(UsuarioNomeAlterado);
		}
	}
}
