using fiapcloudgames.usuario.Domain.Events.Usuario.CreateUsuario;

namespace fiapcloudgames.usuario.Domain.Events.Usuario.DisableUsuario
{
	public record DisableUsuario : DomainEvent
	{
		public string? DesativadoPor { get; set; }
		DisableUsuario() 
		{
			EventType = nameof(CreateUsuario.CreateUsuario);
		}
	}
}