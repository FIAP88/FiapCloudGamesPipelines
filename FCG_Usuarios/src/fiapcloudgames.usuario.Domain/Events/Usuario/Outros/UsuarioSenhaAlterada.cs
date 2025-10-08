namespace fiapcloudgames.usuario.Domain.Events.Usuario.Outros
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