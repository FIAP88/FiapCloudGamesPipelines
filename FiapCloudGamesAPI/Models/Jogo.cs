using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FiapCloudGamesAPI.Entidades;
using System.Text.Json.Serialization;

namespace FiapCloudGamesAPI.Models
{
    public class Jogo(string nome, string descricao, decimal tamanho, int preco, long idCategoria, int idadeMinima, bool ativo, long idFornecedor, string criadoPor) 
        : EntidadeBase(criadoPor)
    {
        public required string Nome { get; set; } = nome;
        public string Descricao { get; set; } = descricao;

        public decimal Tamanho { get; set; } = tamanho;
        public int Preco { get; set; } = preco;

        public long IdCategoria { get; set; } = idCategoria;
        public Categoria Categoria { get; set; }

        public int IdadeMinima { get; set; } = idadeMinima;

        public bool Ativo { get; set; } = ativo;

        public int NumeroVendas { get; set; } = 0;

        public List<string> Tags { get; set; } // para buscas e sugestões

        public long IdFornecedor { get; set; } = idFornecedor;
        public EmpresaFornecedora EmpresaFornecedora { get; set; }

        [JsonIgnore]
        public ICollection<JogoUsuario> UsuariosDoJogo { get; set; }

        public ICollection<Avaliacao> Avaliacoes { get; set; }

    }
}
