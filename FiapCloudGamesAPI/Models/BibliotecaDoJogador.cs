using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class BibliotecaDoJogador(int idUsuario, int idJogo, string criadoPor) : EntidadeBase(criadoPor)
    {
        public int IdUsuario { get; set; } = idUsuario;
        public Usuario Usuario { get; set; }

        public int IdJogo { get; set; } = idJogo;
        public Jogo Jogo { get; set; }

       
    }
}
