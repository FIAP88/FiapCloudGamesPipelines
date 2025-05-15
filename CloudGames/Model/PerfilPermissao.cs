
namespace CloudGames.Model

public class PerfilPermissao { 

    public int IdPerfil { get; set; }
    public int IdPermissao { get; set; }

    public Perfil Perfil { get; set; }
    public Permissao Permissao { get; set; }

}