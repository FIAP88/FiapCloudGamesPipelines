using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CloudGames.Model
{
    public class BibliotecaDoJogador : EntityBase
    {
        
        public int IdUsuario { get; set; }

        public int IdJogo { get; set; }

       
    }
}
