using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class Permissao(string descricao, string criadoPor) : EntidadeBase(criadoPor)
    {
        public string Descricao { get; set; } = descricao;

        public ICollection<PerfilPermissao> PerfilPermissoes { get; set; } = [];
    }
}
