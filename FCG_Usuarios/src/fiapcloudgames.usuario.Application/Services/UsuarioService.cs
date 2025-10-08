using fiapcloudgames.usuario.Application.DTOs;
using fiapcloudgames.usuario.Application.Mappers;
using fiapcloudgames.usuario.Application.Services.Interfaces;
using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Domain.Aggregates;
using fiapcloudgames.usuario.Domain.Interfaces;

//using fiapcloudgames.usuario.Infrastructure.Projections.Projector
//using fiapcloudgames.usuario.Infrastructure.Infraestructure

namespace fiapcloudgames.usuario.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioAggregateRepository _repository;
		private readonly UsuarioAggregateReadModelProjector _projector;
		public UsuarioService(IUsuarioAggregateRepository repository, UsuarioAggregateReadModelProjector projector)
        {
			_repository = repository;
			_projector = projector;
        }

		public async Task CriarUsuarioAsync(CreateUsuarioCommand command)
		{
			// Cria o aggregate
			var aggregateId = new Guid().ToString();
			var usuario = new UsuarioAggregate(
				aggregateId, 
				command.Nome, 
				command.Sobrenome, 
				command.Apelido, 
				command.Email, 
				command.Telefone, 
				command.HashSenha,
				command.DataNascimento, 
				command.PerfilId);

			// Eventos nao comitados
			var eventos = usuario.GetUncommittedEvents().ToList();

			await _repository.SaveAsync(usuario);

			// Atualiza a Projeção
			foreach (var evt in eventos)
			{
				await _projector.Handle(evt);
			}
			// 

		}

		public async Task AlterarEmailAsync(string usuarioId, string novaSenha)
		{
			// Recupera o aggregate do Event Store
			var usuario = await _repository.GetByIdAsync(usuarioId);

			// Executa a operação no aggregate (gera evento)
			usuario.AlterarSenha(novaSenha);

			// Persiste novamente
			await _repository.SaveAsync(usuario);
		}

	}
}
