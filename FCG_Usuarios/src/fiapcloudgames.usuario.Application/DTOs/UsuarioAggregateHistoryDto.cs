namespace fiapcloudgames.usuario.Application.DTOs
{
	public class UsuarioAggregateHistoryDto
	{
		// public Guid Id { get; set; }
		// public string AggregateId { get; set; }
		public required string EventType { get; set; }
		public DateTime Timestamp { get; set; }
		public required string EventData { get; set; }
		public int Version { get; set; }

	}
}
