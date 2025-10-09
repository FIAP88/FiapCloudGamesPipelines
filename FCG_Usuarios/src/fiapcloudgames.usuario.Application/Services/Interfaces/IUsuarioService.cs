using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioEmail;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioNome;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioSobrenome;

namespace fiapcloudgames.usuario.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task CriarUsuarioAsync(CreateUsuarioCommand command);
        Task AlterarNomeAsync(UpdateUsuarioNomeCommand command);
        Task AlterarSobrenomeAsync(UpdateUsuarioSobrenomeCommand command);
        Task AlterarEmailAsync(UpdateUsuarioEmailCommand command);
    }
}
