namespace FiapCloudGamesAPI.Models
{
    public class Perfil(int id, string descricao)
    {
        public int Id { get; set; } = id;
        public string Descricao { get; set; } = descricao;
        public Usuario Usuario { get; set; }
        public ICollection<PerfilPermissao> PerfilPermissoes { get; set; }
    }
}
