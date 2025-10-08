namespace fiapcloudgames.usuario.Application.DTOs
{
    public class UsuarioDto
    {
        public required Guid UsuarioId { get; set; }
        public required string PrimeiroNome { get; set; }
        public required string Sobrenome { get; set; }
        public required string Apelido { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string HashSenha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string? AtualizadoPor { get; set; }
    }
}
