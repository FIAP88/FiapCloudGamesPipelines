using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.Entidades.Dtos
{
    public class UsuarioDto : EntidadeBase
    {
        public required string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public long PerfilId { get; set; }
        public int Idade { get; set; }

    }
}
