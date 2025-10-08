namespace FiapCloudGamesAPI.EventStore.API.Write
{
	public record AtualizarNomeUsuarioCommand
	{		
		public string? NovoNome { get; set; }	
	}
}
