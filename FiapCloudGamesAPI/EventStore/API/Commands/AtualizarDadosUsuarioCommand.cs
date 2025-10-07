namespace FiapCloudGamesAPI.EventStore.API.Write
{
	public record AtualizarDadosUsuarioCommand
	{		
		public string? Apelido { get; set; }
		public DateTime DataNascimento { get; set; }
		public long PerfilId { get; set; }
	}
}
