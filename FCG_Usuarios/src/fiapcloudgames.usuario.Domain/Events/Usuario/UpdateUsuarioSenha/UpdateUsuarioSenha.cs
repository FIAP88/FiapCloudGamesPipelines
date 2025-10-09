namespace fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioSenha
{
	public record UpdateUsuarioSenha : DomainEvent
	{
		public required string? NovoHashSenha { get; set; }
		public UpdateUsuarioSenha()
		{
			EventType = nameof(UpdateUsuarioSenha);
		}
	}
}
