namespace fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioSobrenome
{
	public record UpdateSobrenomeUsuarioCommand
	{
		// public required Guid UsuarioId { get; set; }
		public required string NovoSobrenome { get; set; }
	}
}
