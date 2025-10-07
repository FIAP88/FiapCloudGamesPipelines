using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.EventStore.API.Write
{
	public record AdicionarAvaliaçãoPeloUsuarioCommand
	{
		public int NovaAvaliacao { get; set; }
	}
}
