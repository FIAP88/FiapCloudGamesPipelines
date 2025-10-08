using fiapcloudgames.usuario.Domain.Events;

namespace fiapcloudgames.usuario.Domain.Events.Usuario
{
	public record UsuarioDesativado : DomainEvent
	{
		public string? DesativadoPor { get; set; }
		UsuarioDesativado() 
		{
			EventType = nameof(UsuarioCriado);
		}
	}
}