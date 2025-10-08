using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario
{
	public record UsuarioSobrenomeAlterado : DomainEvent
	{		
		public string? NovoSobrenome { get; set; }
		public UsuarioSobrenomeAlterado()
		{
			EventType = nameof(UsuarioNomeAlterado);
		}
	}
}
