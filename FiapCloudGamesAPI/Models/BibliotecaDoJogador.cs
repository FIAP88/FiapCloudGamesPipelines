using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class BibliotecaDoJogador(long idUsuario, long idJogo, string criadoPor) : EntidadeBase(criadoPor)
    {
        public long IdUsuario { get; set; } = idUsuario;
        public Usuario Usuario { get; set; }

        public long IdJogo { get; set; } = idJogo;
        public Jogo Jogo { get; set; }

       
    }
}
