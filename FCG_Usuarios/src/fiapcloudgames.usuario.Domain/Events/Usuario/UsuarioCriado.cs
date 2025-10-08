using fiapcloudgames.usuario.Domain.Events;

namespace fiapcloudgames.usuario.Domain.Events.Usuario;

public record UsuarioCriado : DomainEvent
{	
	public required string Nome { get; set; }
	public string? Sobrenome { get; set; }
	public string? Apelido { get; set; }
	public string? Email { get; set; }
	public string? Telefone { get; set; }
	public string? HashSenha { get; set; }
	public DateTime DataNascimento { get; set; }
	public long PerfilId { get; set; }

	public UsuarioCriado()
	{
		EventType = nameof(UsuarioCriado);
	}
	
}
