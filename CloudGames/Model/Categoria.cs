using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CloudGames.Model
{
    public class Categoria : EntityBase
    {
        public string Descricao { get; set; }
      
    }
}
