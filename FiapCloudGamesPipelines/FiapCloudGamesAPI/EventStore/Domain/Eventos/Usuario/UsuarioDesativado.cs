using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario
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