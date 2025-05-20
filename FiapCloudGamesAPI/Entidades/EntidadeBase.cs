namespace FiapCloudGamesAPI.Entidades
{
    public class EntidadeBase
    {
        public EntidadeBase(string criadoPor) => this.CriadoPor = criadoPor;

        public int Id { get; set; } = 0;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public string CriadoPor { get; set; }

        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

        public string AtualizadoPor { get; set; } = string.Empty;
    }
}
