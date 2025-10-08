using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario;
using FiapCloudGamesAPI.EventStore.Projection.Model;

namespace FiapCloudGamesAPI.EventStore.Projection.Projector
{
	public class UsuarioAggregateReadModelProjector
	{
		private readonly ReadModelDbContext _context;

		public UsuarioAggregateReadModelProjector(ReadModelDbContext context)
		{
			_context = context;
		}

		public async Task Handle(object evt)
		{
			switch (evt)
			{
				case UsuarioCriado e:
					await Handle(e);
					break;

				case UsuarioNomeAlterado e:
					await Handle(e);
					break;

				case UsuarioEmailAlterado e:
					await Handle(e);
					break;

				default:					
					break;
			}
		}

		public async Task Handle(UsuarioCriado e) 
		{
			_context.Usuarios.Add(new UsuarioAggregateReadModel
			{
				AggregateId = e.AggregateId,
				Nome = e.Nome,
				Email = e.Email							
			});
			await _context.SaveChangesAsync();
		}
		public async Task Handle(UsuarioNomeAlterado e)
		{
			var usuario = await _context.Usuarios.FindAsync(e.AggregateId);
			usuario.Nome = e.NovoNome;
			await _context.SaveChangesAsync();
		}
		public async Task Handle(UsuarioEmailAlterado e)
		{
			var usuario = await _context.Usuarios.FindAsync(e.AggregateId);
			usuario.Email = e.NovoEmail;				
			await _context.SaveChangesAsync();
		}	

	}
}
