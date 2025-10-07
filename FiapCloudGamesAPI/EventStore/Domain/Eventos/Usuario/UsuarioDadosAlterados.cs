using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;
using FiapCloudGamesAPI.Models;
using System.Text.Json.Serialization;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario
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