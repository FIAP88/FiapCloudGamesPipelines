using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.Entidades.Dtos
{
    public class UsuarioDto
    {
        public required string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
        public ICollection<BibliotecaDoJogador> Biblioteca { get; set; } // Relacionamento 1:N
    }
}
