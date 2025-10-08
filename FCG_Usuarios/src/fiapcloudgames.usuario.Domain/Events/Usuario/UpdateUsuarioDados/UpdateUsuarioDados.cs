using System.Text.Json.Serialization;

namespace fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioDados
{
	// Evento para alteração de dados 'genéricos' do usuário
	// que não precisam de uma rastreabilidade completa
	// para justificar o uso de eventos próprios
	public record UpdateUsuarioDados : DomainEvent
	{				
		public string? Apelido { get; set; }
		public DateTime DataNascimento { get; set; }
		public long PerfilId { get; set; }
	
		public UpdateUsuarioDados()
		{
			EventType = nameof(UpdateUsuarioDados);
		}

	}
}