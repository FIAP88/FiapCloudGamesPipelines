using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class Categoria : EntidadeBase
    {
        public string Descricao { get; set; }

        public Jogo Jogo { get; set; }
      
    }
}
