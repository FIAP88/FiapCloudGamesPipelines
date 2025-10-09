namespace fiapcloudgames.usuario.Infrastructure.Projections.ReadModel
{
    public class UsuarioAggregateLoginReadModel
    {
        public required string UsuarioId { get; set; }
        public required string PrimeiroNome { get; set; }
        public required string Sobrenome { get; set; }
        public required string Apelido { get; set; }
        public required string Email { get; set; }
        public required string HashSenha { get; set; }

    }
}
