using fiapcloudgames.usuario.Domain.Aggregates;
using fiapcloudgames.usuario.Domain.Events;

namespace FiapCloudGamesAPI.EventStore.Infraestructure
{
	public interface IUsuarioAggregateRepository
	{
		Task<UsuarioAggregate> GetByIdAsync(string id);
		Task SaveAsync(UsuarioAggregate usuarioAggregate);
		Task<List<DomainEvent>> GetEventsAsync(string aggregateId);
	}

}
