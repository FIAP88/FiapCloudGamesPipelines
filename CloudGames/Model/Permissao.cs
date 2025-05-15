using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CloudGames.Model
{
    public class Permissao : EntityBase
    {
        public string Descricao { get; set; }

        public ICollection<PerfilPermissao> perfilPermissoes { get; set; }
    }
}
