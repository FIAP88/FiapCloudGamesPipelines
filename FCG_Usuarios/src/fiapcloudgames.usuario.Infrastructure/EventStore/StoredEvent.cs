namespace fiapcloudgames.usuario.Infrastructure.EventStore
{ 
	public class StoredEvent
	{
		public Guid Id { get; set; }
		public required string AggregateId { get; set; }
		public int Version { get; set; }
		public DateTime Timestamp { get; set; }
		public required string EventType { get; set; }
		public required string Payload { get; set; }
	}	
}
