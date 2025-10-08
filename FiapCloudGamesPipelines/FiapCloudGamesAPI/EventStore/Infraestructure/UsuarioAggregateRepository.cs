using FiapCloudGamesAPI.EventSourcing.Agregados;
using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;
using FiapCloudGamesAPI.EventStore.Infra;


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

		public async Task SaveAsync(UsuarioAggregate usuarioAggregate)
		{
			var uncommitedEvents = usuarioAggregate.GetUncommittedEvents().ToList();
			
			if (!uncommitedEvents.Any()) return;

			var expectedVersion = usuarioAggregate.Version - uncommitedEvents.Count;

			await _eventStore.SaveEventsAsync(
				usuarioAggregate.Id,
				uncommitedEvents,
				expectedVersion: expectedVersion
			);

			usuarioAggregate.MarkEventsAsCommitted();
		}

		public async Task<List<DomainEvent>> GetEventsAsync(string aggregateId)
		{
			return await _eventStore.GetEventsAsync(aggregateId);
		}
	}
}
