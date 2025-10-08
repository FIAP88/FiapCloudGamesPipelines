using fiapcloudgames.usuario.Domain.Events;

namespace fiapcloudgames.usuario.Domain.Events.Usuario
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