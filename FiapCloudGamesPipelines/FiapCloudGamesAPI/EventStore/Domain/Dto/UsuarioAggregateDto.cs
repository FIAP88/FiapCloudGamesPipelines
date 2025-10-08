namespace FiapCloudGamesAPI.EventStore.Domain.Dto
{
	public class UsuarioAggregateDto 
	{
		public string Id { get; set; }
		public required string Nome { get; set; }
		public string Sobrenome { get; set; }		
		public string Email { get; set; }		
		public DateTime DataNascimento { get; set; }
		public int Version { get; set; }
	}

}
