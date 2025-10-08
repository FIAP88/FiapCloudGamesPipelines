using System.ComponentModel.DataAnnotations.Schema;

namespace FiapCloudGamesAPI.Models
{
    public class JogoUsuario
    {
        public long UsuarioId { get; set; }
        [NotMapped]
        public Usuario Usuario { get; set; }

        public long JogoId { get; set; }

        public Jogo Jogo { get; set; }
    }
}
