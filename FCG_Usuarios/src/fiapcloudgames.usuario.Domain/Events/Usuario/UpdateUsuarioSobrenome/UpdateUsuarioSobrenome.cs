using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioNome;

namespace fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioSobrenome
{
	public record UpdateUsuarioSobrenome : DomainEvent
	{		
		public string? NovoSobrenome { get; set; }
		public UpdateUsuarioSobrenome()
		{
			EventType = nameof(UpdateUsuarioNome.UpdateUsuarioNome);
		}
	}
}
