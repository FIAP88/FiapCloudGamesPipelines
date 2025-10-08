using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.Services.IService
{
    public interface IUsuarioService
    {
        bool UsuarioIsAdmin(Usuario usuario);
        string NomeUsuario(Usuario usuario);
    }
}
