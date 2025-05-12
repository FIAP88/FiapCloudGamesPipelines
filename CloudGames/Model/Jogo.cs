using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CloudGames.Model
{
    public class Jogo : EntityBase
    {

        public required string Nome { get; set; }     
        public string Descricao { get; set; }
  
        public decimal Tamanho { get; set; }
        public int Preco { get; set; }
    
        public int IdCategoria { get; set; }

        public int IdadeMinima { get; set; }

        public bool Ativo { get; set; } = true;

        public int IdFornecedor { get; set; }
        public EmpresaFornecedora EmpresaFornecedora { get; set; }

    }
}
