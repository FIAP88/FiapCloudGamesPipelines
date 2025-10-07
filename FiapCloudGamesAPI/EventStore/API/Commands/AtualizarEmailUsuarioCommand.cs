namespace FiapCloudGamesAPI.EventStore.API.Write
{
	public record AtualizarEmailUsuarioCommand
	{		
		public string? NovoEmail { get; set; }
	}
}
