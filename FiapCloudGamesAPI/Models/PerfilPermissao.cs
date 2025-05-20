
namespace FiapCloudGamesAPI.Models
{
    public class PerfilPermissao(int idPerfil, int idPermissao)
    {
        public int IdPerfil { get; set; } = idPerfil;
        public int IdPermissao { get; set; } = idPermissao;

        public Perfil Perfil { get; set; }
        public Permissao Permissao { get; set; }

    }
}