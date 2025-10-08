namespace fiapcloudgames.usuario.Application.UseCases.Usuario.Outros
{
	public record AtualizarSenhaUsuarioCommand
	{		
		public string? NovoHashSenha { get; set; }
	}
}
