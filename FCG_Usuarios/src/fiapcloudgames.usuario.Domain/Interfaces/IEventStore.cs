using fiapcloudgames.usuario.Domain.Events;

namespace fiapcloudgames.usuario.Domain.Interfaces
{
		// Interface defining the contract for our Event Store
		public interface IEventStore
		{
			Task SaveEventsAsync(string aggregateId, IEnumerable<DomainEvent> events, int expectedVersion);
			Task<List<DomainEvent>> GetEventsAsync(string aggregateId);
			Task<List<DomainEvent>> GetEventsAsync(string aggregateId, int fromVersion);
		}

}
