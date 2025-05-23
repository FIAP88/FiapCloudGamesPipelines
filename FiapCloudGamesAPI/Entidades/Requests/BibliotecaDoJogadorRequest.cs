using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.Entidades.Requests
{
    public class BibliotecaDoJogadorRequest : BaseRequest
    {
        public long IdUsuario { get; set; }
        public long IdJogo { get; set; }
    }
}
