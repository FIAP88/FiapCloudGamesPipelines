using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace fiapcloudgames.usuario.Infrastructure.Persistence.Factories
{
	public class EventStoreDbContextFactory : IDesignTimeDbContextFactory<EventStoreDbContext>
	{
		public EventStoreDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<EventStoreDbContext>();

			optionsBuilder.UseSqlServer(
				"Server=localhost\\SQLExpress;Database=eventstore;Trusted_Connection=true;TrustServerCertificate=true"
			);

			return new EventStoreDbContext(optionsBuilder.Options);
		}
	}
}
