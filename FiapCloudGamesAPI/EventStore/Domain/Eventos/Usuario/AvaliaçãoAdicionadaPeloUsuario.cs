using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario
{
	public record AvaliaçãoAdicionadaPeloUsuario : DomainEvent
	{
		public ICollection<Avaliacao>? Avaliacoes { get; set; }
		public AvaliaçãoAdicionadaPeloUsuario()
		{
			EventType = nameof(AvaliaçãoAdicionadaPeloUsuario);
		}
	}
}
