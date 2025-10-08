namespace FiapCloudGamesAPI.EventStore.API.Write
{
	public record DesativarUsuarioCommand
	{		
		public string? DesativadoPor { get; set; }
	}
}
