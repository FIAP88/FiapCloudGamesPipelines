using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.Entidades.Dtos
{
    public class JogoDto : EntidadeBase
    {
        public required string Nome { get; set; }
        public string Descricao { get; set; }

        public decimal Tamanho { get; set; } 
        public int Preco { get; set; } 

        public long IdCategoria { get; set; } 

        public long IdadeMinima { get; set; }

        public bool Ativo { get; set; } 

        public long IdFornecedor { get; set; } 
    }
}
