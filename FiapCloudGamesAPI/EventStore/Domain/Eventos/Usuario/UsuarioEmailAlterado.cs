using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario
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
