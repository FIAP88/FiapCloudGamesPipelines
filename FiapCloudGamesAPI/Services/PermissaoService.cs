using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.Services
{
    public class PermissaoService
    {
        UsuarioService _usuarioService = new UsuarioService();

        public bool UsuarioTemPermissao(Usuario usuario, HttpRequest request)
        {
            if (request.Method != "GET")
            {
                return _usuarioService.UsuarioIsAdmin(usuario);
            }
            return true;
        }
    }
}
