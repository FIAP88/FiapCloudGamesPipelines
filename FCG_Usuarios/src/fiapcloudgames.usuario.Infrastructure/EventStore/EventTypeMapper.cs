using fiapcloudgames.usuario.Domain.Events;
using System.Reflection;

namespace fiapcloudgames.usuario.Infrastructure.EventStore
{
	public static class EventTypeMapper
	{
		private static readonly Dictionary<string, Type> _eventTypes = new();

		static EventTypeMapper()
		{
		
			var domainEventType = typeof(DomainEvent);

			var eventTypes = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => domainEventType.IsAssignableFrom(t) && !t.IsAbstract);

			foreach (var type in eventTypes)
			{
				_eventTypes[type.Name] = type;
			}
		}

		public static Type GetTypeFor(string eventTypeName)
		{
			if (_eventTypes.TryGetValue(eventTypeName, out var type))
			{
				return type;
			}

			throw new InvalidOperationException($"Evento '{eventTypeName}' não mapeado.");
		}

		public static string GetNameFor(Type eventType) => eventType.Name;
	}

}