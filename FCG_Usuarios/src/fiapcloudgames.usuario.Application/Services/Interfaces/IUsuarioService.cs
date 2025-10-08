using fiapcloudgames.usuario.Application.DTOs;
using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuario;

namespace fiapcloudgames.usuario.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        IEnumerable<UsuarioDto> ObterTodos();
        UsuarioDto? ObterPorId(Guid id);
        UsuarioDto Cadastrar(CreateUsuarioCommand dto);
        UsuarioDto? Alterar(UpdateUsuarioCommand dto, string alteradoPor);
        bool Deletar(Guid id);
    }
}
