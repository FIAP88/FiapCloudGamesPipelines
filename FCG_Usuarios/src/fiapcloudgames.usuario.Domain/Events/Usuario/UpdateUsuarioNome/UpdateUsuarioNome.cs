namespace fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioNome
{
	public record UpdateUsuarioNome : DomainEvent
	{
		public string? NovoNome { get; set; }
		public UpdateUsuarioNome()
		{
			EventType = nameof(UpdateUsuarioNome);
		}
	}
}
