using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CloudGames.Model
{
    public class EmpresaFornecedora : EntityBase
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }

      public Jogo Jogo { get; set; }
    }
}
