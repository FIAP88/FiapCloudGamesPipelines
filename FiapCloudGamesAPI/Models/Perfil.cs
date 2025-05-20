namespace FiapCloudGamesAPI.Models
{
    public class Perfil
    {
        public int Id { get; set; }

        public string Descricao { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<PerfilPermissao> PerfilPermissoes { get; set; }
    }
}
