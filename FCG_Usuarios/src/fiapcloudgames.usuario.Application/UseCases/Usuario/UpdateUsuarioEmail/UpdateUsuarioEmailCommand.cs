namespace fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioEmail
{
	public record UpdateUsuarioEmailCommand
	{
		public required Guid UsuarioId { get; set; }
		public required string NovoEmail { get; set; }
	}
}
