using fiapcloudgames.usuario.Domain.Events;

namespace fiapcloudgames.usuario.Domain.Interfaces
{
    public interface IEventDispatcher
    {
        Task PublishAsync(IEnumerable<DomainEvent> events);
    }
} 