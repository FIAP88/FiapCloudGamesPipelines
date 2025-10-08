using fiapcloudgames.usuario.Domain.Entities;

namespace fiapcloudgames.usuario.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        IEnumerable<T> ObterTodos();
        T? ObterPorId(Guid id);
        T Cadastrar(T entidade);
        T Alterar(T entidade);
        void Deletar(Guid id);
    }
}
