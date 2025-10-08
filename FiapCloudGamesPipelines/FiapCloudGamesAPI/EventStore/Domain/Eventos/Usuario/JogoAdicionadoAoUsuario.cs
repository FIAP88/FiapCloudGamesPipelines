using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario
{
	public record JogoAdicionadoAoUsuario : DomainEvent
	{
		public ICollection<JogoUsuario>? JogosDoUsuario { get; set; }
		public JogoAdicionadoAoUsuario()
		{
			EventType = nameof(JogoAdicionadoAoUsuario);
		}
	}
}
