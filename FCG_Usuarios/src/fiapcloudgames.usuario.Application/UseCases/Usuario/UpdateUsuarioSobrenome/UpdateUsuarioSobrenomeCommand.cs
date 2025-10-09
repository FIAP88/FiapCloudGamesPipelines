namespace fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioSobrenome
{
	public record UpdateUsuarioSobrenomeCommand
	{
		public required Guid UsuarioId { get; set; }
		public required string NovoSobrenome { get; set; }
	}
}
