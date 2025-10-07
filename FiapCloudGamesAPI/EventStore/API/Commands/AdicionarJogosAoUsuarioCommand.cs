using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.EventStore.API.Write
{
	public record AdicionarJogosAoUsuarioCommand
	{		
		public IEnumerable<int>? NovosJogosDoUsuario { get; set; }
	}
}
