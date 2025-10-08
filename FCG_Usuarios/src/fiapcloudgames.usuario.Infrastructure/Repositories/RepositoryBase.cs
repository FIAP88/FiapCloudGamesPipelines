using fiapcloudgames.usuario.Domain.Entities;
using fiapcloudgames.usuario.Domain.Interfaces;
using fiapcloudgames.usuario.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace fiapcloudgames.usuario.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected FiapCloudGamesDbContext _context;
        protected DbSet<T> _dbSet;

        public RepositoryBase(FiapCloudGamesDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Alterar(T entidade)
        {
            entidade.DataAtualizacao = DateTime.UtcNow;
            _context.Set<T>().Update(entidade);
            _context.SaveChanges();
            return entidade;
        }

        public T Cadastrar(T entidade)
        {
            entidade.DataCadastro = DateTime.UtcNow;
            _context.Set<T>().Add(entidade);
            _context.SaveChanges();
            return entidade;
        }

        public void Deletar(Guid id)
        {
            _context.Set<T>().Remove(ObterPorId(id)!);
            _context.SaveChanges();
        }

        public T? ObterPorId(Guid id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> ObterTodos()
        {
            return _context.Set<T>().ToList();
        }
    }
}
