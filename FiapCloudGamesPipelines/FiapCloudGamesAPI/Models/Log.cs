using FiapCloudGamesAPI.Entidades;

namespace FiapCloudGamesAPI.Models
{
    public class Log(string mensagem, string criadoPor) : EntidadeBase(criadoPor)
    {
        public string Mensagem { get; set; } = mensagem;
        public DateTime DataHora { get; set; } = DateTime.UtcNow;
    }
}
