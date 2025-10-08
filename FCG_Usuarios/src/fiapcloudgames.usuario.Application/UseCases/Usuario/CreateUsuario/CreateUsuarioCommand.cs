namespace fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario
{
    public class CreateUsuarioCommand
    {
        public required string PrimeiroNome { get; set; }
        public required string Sobrenome { get; set; }
        public required string Apelido { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string HashSenha { get; set; }
    }
}
