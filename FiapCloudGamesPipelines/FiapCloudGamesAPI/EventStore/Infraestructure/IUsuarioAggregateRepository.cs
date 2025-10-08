using FiapCloudGamesAPI.EventSourcing.Agregados;
using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesAPI.EventStore.Infraestructure
{
	public interface IUsuarioAggregateRepository
	{
		Task<UsuarioAggregate> GetByIdAsync(string id);
		Task SaveAsync(UsuarioAggregate usuarioAggregate);
		Task<List<DomainEvent>> GetEventsAsync(string aggregateId);
	}


}
