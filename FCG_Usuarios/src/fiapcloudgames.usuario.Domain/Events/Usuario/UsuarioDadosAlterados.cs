using fiapcloudgames.usuario.Domain.Events;
using System.Text.Json.Serialization;

namespace fiapcloudgames.usuario.Domain.Events.Usuario
{
	// Evento para alteração de dados 'genéricos' do usuário
	// que não precisam de uma rastreabilidade completa
	// para justificar o uso de eventos próprios
	public record UsuarioDadosAlterados : DomainEvent
	{				
		public string? Apelido { get; set; }
		public DateTime DataNascimento { get; set; }
		public long PerfilId { get; set; }
	
		public UsuarioDadosAlterados()
		{
			EventType = nameof(UsuarioDadosAlterados);
		}

	}
}