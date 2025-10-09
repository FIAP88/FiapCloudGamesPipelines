namespace fiapcloudgames.usuario.Application.UseCases.Usuario.DisableUsuario
{
	public record DisableUsuarioCommand
	{
        public required Guid UsuarioId { get; set; }
        public string? DesativadoPor { get; set; }
	}
}
