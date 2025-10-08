namespace fiapcloudgames.usuario.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string? AtualizadoPor { get; set; }
    }
}
