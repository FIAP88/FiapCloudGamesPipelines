using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class Usuario(string nome, string sobrenome, string apelido, string email, string hashSenha, DateTime dataNascimento, int perfilId, string criadoPor) 
        : EntidadeBase(criadoPor)
    {
        public required string Nome { get; set; } = nome;
        public string Sobrenome { get; set; } = sobrenome;
        public string Apelido { get; set; } = apelido;
        public string Email { get; set; } = email;
        public string HashSenha { get; set; } = hashSenha;
        public DateTime DataNascimento { get; set; } = dataNascimento;
        public int PerfilId { get; set; } = perfilId;
        public Perfil Perfil { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
        public BibliotecaDoJogador Biblioteca { get; set; }

    }
}
