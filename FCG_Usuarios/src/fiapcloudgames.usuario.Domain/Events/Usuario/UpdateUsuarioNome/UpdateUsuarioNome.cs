namespace fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioNome
{
	public record UpdateUsuarioNome : DomainEvent
	{
		public required string? NovoNome { get; set; }
		public UpdateUsuarioNome()
		{
			EventType = nameof(UpdateUsuarioNome);
		}
	}
}
