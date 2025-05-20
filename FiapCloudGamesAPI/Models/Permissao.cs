using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class Permissao : EntidadeBase
    {
        public string Descricao { get; set; }

        public ICollection<PerfilPermissao> PerfilPermissoes { get; set; }
    }
}
