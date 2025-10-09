using System.Text.Json.Serialization;

namespace fiapcloudgames.usuario.Domain.Events
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