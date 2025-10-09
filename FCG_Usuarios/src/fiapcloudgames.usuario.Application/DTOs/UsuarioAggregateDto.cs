namespace fiapcloudgames.usuario.Application.DTOs
{
	public class UsuarioAggregateDto 
	{
		public required string Id { get; set; }
		public required string Nome { get; set; }
		public required string Sobrenome { get; set; }		
		public required string Email { get; set; }		
		public DateTime DataNascimento { get; set; }
		public int Version { get; set; }
	}

}
