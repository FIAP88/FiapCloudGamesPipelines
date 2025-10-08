using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;
using FiapCloudGamesAPI.Models;

namespace fiapcloudgames.usuario.Domain.Events.Usuario.Outros
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
