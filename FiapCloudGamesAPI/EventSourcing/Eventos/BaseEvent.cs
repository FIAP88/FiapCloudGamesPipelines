namespace FiapCloudGamesAPI.EventSourcing.Eventos
{
	public abstract record BaseEvent(Guid StreamId)
	{
		public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
	}
}
