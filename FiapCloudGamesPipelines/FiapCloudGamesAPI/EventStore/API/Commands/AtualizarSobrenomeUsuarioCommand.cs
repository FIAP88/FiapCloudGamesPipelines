namespace FiapCloudGamesAPI.EventStore.API.Write
{
	public record AtualizarSobrenomeUsuarioCommand
	{		
		public string? NovoSobrenome { get; set; }
	}
}
