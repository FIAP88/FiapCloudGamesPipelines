using fiapcloudgames.usuario.Application.DTOs;
using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioEmail;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioNome;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuarioSobrenome;
using fiapcloudgames.usuario.Domain.Events;

namespace fiapcloudgames.usuario.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task CriarUsuarioAsync(CreateUsuarioCommand command);
        Task AlterarNomeAsync(UpdateUsuarioNomeCommand command);
        Task AlterarSobrenomeAsync(UpdateUsuarioSobrenomeCommand command);
        Task AlterarEmailAsync(UpdateUsuarioEmailCommand command);
        Task<UsuarioAggregateDto> GetByIdAsync(string aggregateId);
		Task<List<UsuarioAggregateHistoryDto>> GetEventsAsync(string aggregateId);
	}
}
