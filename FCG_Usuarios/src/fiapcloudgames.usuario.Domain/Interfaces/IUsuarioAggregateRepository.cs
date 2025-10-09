using fiapcloudgames.usuario.Domain.Aggregates;
using fiapcloudgames.usuario.Domain.Events;

namespace fiapcloudgames.usuario.Domain.Interfaces
{
	public interface IUsuarioAggregateRepository
	{
		Task<UsuarioAggregate> GetByIdAsync(string id);
		Task<List<DomainEvent>> SaveAsync(UsuarioAggregate usuarioAggregate);
		Task<List<DomainEvent>> GetEventsAsync(string aggregateId);
	}

}
