using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class EmpresaFornecedora : EntidadeBase
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }

      public Jogo Jogo { get; set; }
    }
}
