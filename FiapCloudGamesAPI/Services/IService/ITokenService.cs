using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.Services.IService
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);

        long GetUsuarioId(string token);
    }
}
