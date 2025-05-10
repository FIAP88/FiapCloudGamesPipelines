using System.ComponentModel.DataAnnotations;

namespace FiapCloudGamesAPI.Models
{
    public class Usuario
    {
        [Key]
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public required long UserId { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public required string Name { get; set; }

        public int IdPerfil { get; set; }
        public string Senha { get; set; }
    }
}
