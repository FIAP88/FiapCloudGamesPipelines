namespace FiapCloudGamesAPI.EventStore.Domain.Dto
{
	public class UsuarioAggregateHistoryDto
	{
		// public Guid Id { get; set; }
		// public string AggregateId { get; set; }
		public string EventType { get; set; }
		public DateTime Timestamp { get; set; }
		public string EventData { get; set; }
		public int Version { get; set; }

	}
}
