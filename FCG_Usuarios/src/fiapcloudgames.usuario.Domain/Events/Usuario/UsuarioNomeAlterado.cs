using fiapcloudgames.usuario.Domain.Events;

namespace fiapcloudgames.usuario.Domain.Events.Usuario
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
