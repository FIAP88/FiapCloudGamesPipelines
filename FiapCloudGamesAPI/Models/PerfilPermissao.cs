
namespace FiapCloudGamesAPI.Models
{
    public class PerfilPermissao(long idPerfil, long idPermissao)
    {
        public long IdPerfil { get; set; } = idPerfil;
        public long IdPermissao { get; set; } = idPermissao;

        public Perfil Perfil { get; set; }
        public Permissao Permissao { get; set; }

    }
}