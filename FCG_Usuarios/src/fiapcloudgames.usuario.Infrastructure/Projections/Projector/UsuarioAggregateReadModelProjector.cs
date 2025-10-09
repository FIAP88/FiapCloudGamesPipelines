using fiapcloudgames.usuario.Domain.Events.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioEmail;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioNome;
using fiapcloudgames.usuario.Domain.Interfaces;
using fiapcloudgames.usuario.Infrastructure.Persistence;
using fiapcloudgames.usuario.Infrastructure.Projections.ReadModel;

namespace fiapcloudgames.usuario.Infrastructure.Projections.Projector
{
	public class UsuarioAggregateReadModelProjector : IProjector
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
				case CreateUsuario e:
					await Handle(e);
					break;

				case UpdateUsuarioNome e:
					await Handle(e);
					break;

				case UpdateUsuarioEmail e:
					await Handle(e);
					break;

				default:					
					break;
			}
		}

		public async Task Handle(CreateUsuario e) 
		{
			_context.Usuarios.Add(new UsuarioAggregateReadModel
			{
				AggregateId = e.AggregateId,
				Nome = e.Nome,
				Email = e.Email							
			});
			await _context.SaveChangesAsync();
		}
		public async Task Handle(UpdateUsuarioNome e)
		{
			var usuario = await _context.Usuarios.FindAsync(e.AggregateId);
			usuario.Nome = e.NovoNome;
			await _context.SaveChangesAsync();
		}
		public async Task Handle(UpdateUsuarioEmail e)
		{
			var usuario = await _context.Usuarios.FindAsync(e.AggregateId);
			usuario.Email = e.NovoEmail;				
			await _context.SaveChangesAsync();
		}	

	}
}
