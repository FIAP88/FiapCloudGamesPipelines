using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesAPI.Services.IService;

namespace FiapCloudGamesAPI.Services
{
    public class UsuarioService
	{
        public bool UsuarioIsAdmin(Usuario usuario) => usuario.PerfilId == 1;

        public string ObterHashSenha(string senha)
        {
            return senha;
        }

    }
}
