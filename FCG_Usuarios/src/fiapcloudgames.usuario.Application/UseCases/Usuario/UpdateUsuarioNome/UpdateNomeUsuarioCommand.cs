namespace fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioNome
{
	public record UpdateNomeUsuarioCommand
	{
		// public required Guid UsuarioId { get; set; }
		public required string NovoNome { get; set; }	
	}
}
