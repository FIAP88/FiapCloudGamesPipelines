namespace fiapcloudgames.usuario.Application.UseCases.Usuario.Outros
{
	public record AdicionarJogosAoUsuarioCommand
	{		
		public IEnumerable<int>? NovosJogosDoUsuario { get; set; }
	}
}
