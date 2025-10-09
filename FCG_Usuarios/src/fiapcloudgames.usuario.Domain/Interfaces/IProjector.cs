namespace fiapcloudgames.usuario.Domain.Interfaces
{
    public interface IProjector
    {
        Task Handle(object evt);
    }
}