using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CloudGames.Model
{
    public class Avaliacao : EntityBase
    {
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public int IdJogo { get; set; }
        public Jogo Jogo { get; set; }

        public int Nota { get; set; }

        public string Comentario { get; set; }

       
    }
}
