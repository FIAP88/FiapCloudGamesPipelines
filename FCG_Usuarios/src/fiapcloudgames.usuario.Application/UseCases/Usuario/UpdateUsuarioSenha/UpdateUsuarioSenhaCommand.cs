namespace fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioSenha
{
	public record UpdateUsuarioSenhaCommand
	{
		public required Guid UsuarioId { get; set; }
		public required string NovoHashSenha { get; set; }	
	}
}
