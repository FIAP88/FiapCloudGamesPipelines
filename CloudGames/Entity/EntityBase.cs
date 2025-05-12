namespace CloudGames.Model
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }

        public string CriadoPor { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public string AtualizadoPor { get; set; }
    }
}
