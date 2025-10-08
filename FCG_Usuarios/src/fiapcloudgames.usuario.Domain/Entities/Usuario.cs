namespace fiapcloudgames.usuario.Domain.Entities
{
    public class Usuario : EntityBase
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
