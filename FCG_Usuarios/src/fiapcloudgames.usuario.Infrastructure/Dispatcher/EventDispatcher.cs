using fiapcloudgames.usuario.Domain.Events;
using fiapcloudgames.usuario.Domain.Interfaces;

namespace fiapcloudgames.usuario.Infrastructure.Dispatcher
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IEnumerable<IProjector> _projectors;

        public EventDispatcher(IEnumerable<IProjector> projectors)
        {
            _projectors = projectors;
        }

        public async Task PublishAsync(IEnumerable<DomainEvent> events)
        {
            foreach (var evt in events)
            {
                foreach (var projector in _projectors)
                {
                    await projector.Handle(evt);
                }
            }
        }
    }
}