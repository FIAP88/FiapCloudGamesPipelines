using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Dapper;
using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;

namespace FiapCloudGamesAPI.EventStore.Infra
{
		// Interface defining the contract for our Event Store
		public interface IEventStore
		{
			Task SaveEventsAsync(string aggregateId, IEnumerable<DomainEvent> events, int expectedVersion);
			Task<List<DomainEvent>> GetEventsAsync(string aggregateId);
			Task<List<DomainEvent>> GetEventsAsync(string aggregateId, int fromVersion);
		}

}
