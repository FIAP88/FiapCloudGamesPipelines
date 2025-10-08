using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario;

public record UsuarioCriado : DomainEvent
{	
	public required string Nome { get; set; }
	public string? Sobrenome { get; set; }
	public string? Apelido { get; set; }
	public string? Email { get; set; }
	public string? HashSenha { get; set; }
	public DateTime DataNascimento { get; set; }
	public long PerfilId { get; set; }

	public UsuarioCriado()
	{
		EventType = nameof(UsuarioCriado);
	}
	
}
