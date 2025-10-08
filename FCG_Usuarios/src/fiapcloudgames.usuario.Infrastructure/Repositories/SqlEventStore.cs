using fiapcloudgames.usuario.Domain.Events;
using fiapcloudgames.usuario.Domain.Interfaces;
using fiapcloudgames.usuario.Infrastructure.EventStore;
using fiapcloudgames.usuario.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
//using Dapper;

namespace FiapCloudGamesAPI.EventStore.Infra
{
	public class SqlEventStore : IEventStore
	{
		private readonly EventStoreDbContext _db;

		public SqlEventStore(EventStoreDbContext db)
		{
			_db = db;
		}

		public async Task SaveEventsAsync(string aggregateId, IEnumerable<DomainEvent> events, int expectedVersion)
		{
		
			// pega a última versão salva
			var lastVersion = await _db.Events
				.Where(e => e.AggregateId == aggregateId)
				.OrderByDescending(e => e.Version)
				.Select(e => e.Version)
				.FirstOrDefaultAsync();

			if (lastVersion != expectedVersion && lastVersion != 0)
				throw new InvalidOperationException("Concurrency conflict detected.");

			foreach (var evt in events)
			{
				var payload = JsonConvert.SerializeObject(evt);

				var stored = new StoredEvent
				{
					Id = evt.Id,
					AggregateId = aggregateId,
					Version = ++expectedVersion,
					Timestamp = evt.Timestamp,
					EventType = evt.GetType().Name,
					Payload = payload
				};

				_db.Events.Add(stored);
			}

			await _db.SaveChangesAsync();
		}

		public async Task<List<DomainEvent>> GetEventsAsync(string aggregateId)
		{
			var storedEvents = await _db.Events
				.Where(e => e.AggregateId == aggregateId)
				.OrderBy(e => e.Version)
				.ToListAsync();
			
			var events = new List<DomainEvent>();
			foreach (var e in storedEvents)
			{
				// Pega o tipo correto usando o mapper
				var eventType = EventTypeMapper.GetTypeFor(e.EventType);

				// Desserializa apenas os campos do evento derivado
				var evt = JsonConvert.DeserializeObject(e.Payload, eventType)! as DomainEvent;
				
				// Salva os dados do StoredEvent
				evt.AggregateId = e.AggregateId;
				evt.Version = e.Version;

				events.Add(evt);
			}
			return events;	
		}

		public async Task<List<DomainEvent>> GetEventsAsync(string aggregateId, int fromVersion)
		{
			var storedEvents = await _db.Events
				.Where(e => e.AggregateId == aggregateId && e.Version > fromVersion)
				.OrderBy(e => e.Version)
				.ToListAsync();

			////
			var events = new List<DomainEvent>();
			foreach (var e in storedEvents)
			{
				// Pega o tipo correto usando o mapper
				var type = EventTypeMapper.GetTypeFor(e.EventType);

				// Desserializa apenas os campos do evento derivado
				var evt = JsonConvert.DeserializeObject(e.Payload, type)! as DomainEvent;

				events.Add(evt);
			}
			return events;

		}

	}

}