using fiapcloudgames.usuario.Domain.Entities;
using fiapcloudgames.usuario.Domain.Interfaces;
using fiapcloudgames.usuario.Infrastructure.Persistence;

namespace fiapcloudgames.usuario.Infrastructure.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(FiapCloudGamesDbContext context) : base(context)
        {
        }

    }
}
