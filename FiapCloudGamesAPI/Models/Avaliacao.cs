using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FiapCloudGamesAPI.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGamesAPI.Models
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Avaliacao(long idUsuario, long idJogo, int nota, string comentario, string criadoPor) : EntidadeBase(criadoPor)
    {
        public long IdUsuario { get; set; } = idUsuario;
        public Usuario Usuario { get; set; }

        public long IdJogo { get; set; } = idJogo;
        public Jogo Jogo { get; set; }

        public int Nota { get; set; } = nota;

        public string Comentario { get; set; } = comentario;
    }
}
