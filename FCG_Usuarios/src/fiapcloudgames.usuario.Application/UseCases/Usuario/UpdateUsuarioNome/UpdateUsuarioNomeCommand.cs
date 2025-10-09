namespace fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioNome
{
	public record UpdateUsuarioSenhaCommand
	{
		public required Guid UsuarioId { get; set; }
		public required string NovoNome { get; set; }	
	}
}
