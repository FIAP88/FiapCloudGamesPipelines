using fiapcloudgames.usuario.Domain.Events;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace fiapcloudgames.usuario.Domain.Events.Usuario
{
	public record UsuarioEmailAlterado : DomainEvent
	{
		public string? NovoEmail { get; set; }
		public UsuarioEmailAlterado()
		{
			EventType = nameof(UsuarioEmailAlterado);
		}
	}
}
