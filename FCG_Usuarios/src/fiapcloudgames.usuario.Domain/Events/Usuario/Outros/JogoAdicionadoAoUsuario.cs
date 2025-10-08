using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;
using FiapCloudGamesAPI.Models;

namespace fiapcloudgames.usuario.Domain.Events.Usuario.Outros
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
