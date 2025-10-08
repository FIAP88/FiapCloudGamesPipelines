//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioEmail
{
	public record UpdateUsuarioEmail : DomainEvent
	{
		public string? NovoEmail { get; set; }
		public UpdateUsuarioEmail()
		{
			EventType = nameof(UpdateUsuarioEmail);
		}
	}
}
