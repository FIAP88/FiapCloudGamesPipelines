public class StoredEvent
{
	public Guid Id { get; set; }
	public string AggregateId { get; set; }
	public int Version { get; set; }
	public DateTime Timestamp { get; set; }
	public string EventType { get; set; }
	public string Payload { get; set; }
}	