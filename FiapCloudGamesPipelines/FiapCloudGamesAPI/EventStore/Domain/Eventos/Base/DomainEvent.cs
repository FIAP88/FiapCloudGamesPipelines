using Newtonsoft.Json;
using System.Reflection;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Base
{
	public abstract record DomainEvent()
	{
		[JsonIgnore] public Guid Id { get; set; } = Guid.NewGuid();
		[JsonIgnore] public string AggregateId { get; set; }
		[JsonIgnore] public int Version { get; set; }
		[JsonIgnore] public DateTime Timestamp { get; set; } = DateTime.UtcNow;
		[JsonIgnore] public string EventType { get; set; }

	}
}