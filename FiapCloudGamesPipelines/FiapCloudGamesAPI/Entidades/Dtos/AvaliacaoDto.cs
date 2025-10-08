using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.Entidades.Dtos
{
    public class AvaliacaoDto : EntidadeBase
    {
        public long IdUsuario { get; set; }

        public long IdJogo { get; set; }

        public int Nota { get; set; }

        public string Comentario { get; set; }
    }
}
