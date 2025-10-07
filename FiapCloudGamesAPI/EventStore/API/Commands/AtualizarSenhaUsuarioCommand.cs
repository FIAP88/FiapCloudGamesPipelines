namespace FiapCloudGamesAPI.EventStore.API.Write
{
	public record AtualizarSenhaUsuarioCommand
	{		
		public string? NovoHashSenha { get; set; }
	}
}
