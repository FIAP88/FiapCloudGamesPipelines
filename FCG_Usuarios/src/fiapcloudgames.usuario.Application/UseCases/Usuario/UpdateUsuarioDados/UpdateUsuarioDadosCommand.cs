namespace fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioDados
{
	public record UpdateUsuarioDadosCommand
	{
		public required Guid UsuarioId { get; set; }
		public string? Apelido { get; set; }
		public DateTime DataNascimento { get; set; }
		public long PerfilId { get; set; }
	}
}
