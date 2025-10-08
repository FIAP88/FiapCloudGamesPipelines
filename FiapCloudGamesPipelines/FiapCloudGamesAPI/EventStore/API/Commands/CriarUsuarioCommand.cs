using FiapCloudGamesAPI.Models;
using System.Text.Json.Serialization;

namespace FiapCloudGamesAPI.EventStore.API.Write
{
	public record CriarUsuarioCommand
	{		
		public required string Nome { get; set; }
		public string? Sobrenome { get; set; }
		public string? Apelido { get; set; }
		public string? Email { get; set; }		
		public string? HashSenha { get; set; }
		public DateTime DataNascimento { get; set; }
		public long PerfilId { get; set; }

	}
}
