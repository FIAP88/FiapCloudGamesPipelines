using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class Avaliacao : EntidadeBase
    {
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public int IdJogo { get; set; }
        public Jogo Jogo { get; set; }

        public int Nota { get; set; }

        public string Comentario { get; set; }

       
    }
}
