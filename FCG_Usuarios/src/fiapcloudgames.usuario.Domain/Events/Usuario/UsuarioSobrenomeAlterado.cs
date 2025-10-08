using fiapcloudgames.usuario.Domain.Events;

namespace fiapcloudgames.usuario.Domain.Events.Usuario
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
