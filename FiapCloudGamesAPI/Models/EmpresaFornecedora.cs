using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class EmpresaFornecedora(string nome, string cNPJ, string criadoPor) : EntidadeBase(criadoPor)
    {
        public string Nome { get; set; } = nome;
        public string CNPJ { get; set; } = cNPJ;

        public ICollection<Jogo> Jogos { get; set; }

    }
}
