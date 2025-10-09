using fiapcloudgames.usuario.Domain.Aggregates;
using fiapcloudgames.usuario.Domain.Events;
using fiapcloudgames.usuario.Domain.Interfaces;

namespace FiapCloudGamesAPI.EventStore.Infraestructure
{
	public class UsuarioAggregateRepository : IUsuarioAggregateRepository
	{
		private readonly IEventStore _eventStore;

		public UsuarioAggregateRepository(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public async Task<UsuarioAggregate> GetByIdAsync(string id)
		{
			var history = await _eventStore.GetEventsAsync(id);
			if (!history.Any())
				return null;

			return UsuarioAggregate.FromHistory(history);
		}

		public async Task<List<DomainEvent>> SaveAsync(UsuarioAggregate usuarioAggregate)
		{
			var uncommitedEvents = usuarioAggregate.GetUncommittedEvents().ToList();

			var expectedVersion = usuarioAggregate.Version - uncommitedEvents.Count;

			await _eventStore.SaveEventsAsync(
				usuarioAggregate.Id,
				uncommitedEvents,
				expectedVersion: expectedVersion
			);

			usuarioAggregate.MarkEventsAsCommitted();

            return uncommitedEvents;
		}

		public async Task<List<DomainEvent>> GetEventsAsync(string aggregateId)
		{
			return await _eventStore.GetEventsAsync(aggregateId);
		}
	}
}
