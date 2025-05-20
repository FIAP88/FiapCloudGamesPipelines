using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class Jogo : EntidadeBase
    {

        public required string Nome { get; set; }     
        public string Descricao { get; set; }
  
        public decimal Tamanho { get; set; }
        public int Preco { get; set; }
    
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }

        public int IdadeMinima { get; set; }

        public bool Ativo { get; set; } = true;

        public int IdFornecedor { get; set; }
        public EmpresaFornecedora EmpresaFornecedora { get; set; }

        public ICollection<BibliotecaDoJogador> Bibliotecas { get; set; }

        public ICollection<Avaliacao> Avaliacoes { get; set; }

    }
}
