using fiapcloudgames.usuario.Domain.Events.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioEmail;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioNome;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioSenha;
using fiapcloudgames.usuario.Domain.Interfaces;
using fiapcloudgames.usuario.Infrastructure.Persistence;
using fiapcloudgames.usuario.Infrastructure.Projections.ReadModel;

namespace fiapcloudgames.usuario.Infrastructure.Projections.Projector
{
    public class UsuarioAggregateLoginReadModelProjector : IProjector
    {
        private readonly ReadModelDbContext _context;

        public UsuarioAggregateLoginReadModelProjector(ReadModelDbContext context)
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

                case UpdateUsuarioSenha e:
                    await Handle(e);
                    break;

                default:
                    break;
            }
        }

        public async Task Handle(CreateUsuario e)
        {
            _context.UsuariosLogin.Add(new UsuarioAggregateLoginReadModel
            {
                UsuarioId = e.AggregateId,
                PrimeiroNome = e.Nome,
                Sobrenome = e.Sobrenome,
                Apelido = e.Apelido,
                Email = e.Email,
                HashSenha = e.HashSenha
            });
            await _context.SaveChangesAsync();
        }
        public async Task Handle(UpdateUsuarioNome e)
        {
            var usuario = await _context.UsuariosLogin.FindAsync(e.AggregateId);
            usuario.PrimeiroNome = e.NovoNome;
            await _context.SaveChangesAsync();
        }
        public async Task Handle(UpdateUsuarioEmail e)
        {
            var usuario = await _context.UsuariosLogin.FindAsync(e.AggregateId);
            usuario.Email = e.NovoEmail;
            await _context.SaveChangesAsync();
        }
        public async Task Handle(UpdateUsuarioSenha e)
        {
            var usuario = await _context.UsuariosLogin.FindAsync(e.AggregateId);
            usuario.HashSenha = e.NovoHashSenha;
            await _context.SaveChangesAsync();
        }

    }
}
