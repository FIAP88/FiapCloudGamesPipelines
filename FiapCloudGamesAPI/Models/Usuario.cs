using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class Usuario : EntidadeBase
    {
      
        public required string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string HashSenha { get; set; }
        public int? DataNascimento { get; set; }
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
        public ICollection<BibliotecaDoJogador> Biblioteca { get; set; } // Relacionamento 1:N

    }
}
