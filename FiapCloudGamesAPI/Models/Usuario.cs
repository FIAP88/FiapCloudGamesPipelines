using FiapCloudGamesAPI.Entidades;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;


namespace FiapCloudGamesAPI.Models
{
    public class Usuario(string nome, string sobrenome, string apelido, string email, string hashSenha, DateTime dataNascimento, long perfilId, string criadoPor) 
        : EntidadeBase(criadoPor)
    {
        public required string Nome { get; set; } = nome;
        public string Sobrenome { get; set; } = sobrenome;
        public string Apelido { get; set; } = apelido;
        public string Email { get; set; } = email;
        [JsonIgnore]
        public string HashSenha { get; set; } = hashSenha;
        public DateTime DataNascimento { get; set; } = dataNascimento;
        public long PerfilId { get; set; } = perfilId;
        public Perfil Perfil { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
        [JsonIgnore]
        public ICollection<JogoUsuario> JogosDoUsuario { get; set; }

        [NotMapped]
        [JsonProperty(nameof(Idade))]
        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;
                if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
                return idade;
            }
        }
        

    }
}
