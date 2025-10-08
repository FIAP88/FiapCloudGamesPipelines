using fiapcloudgames.usuario.Infrastructure.EventStore;
using fiapcloudgames.usuario.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace fiapcloudgames.usuario.Infrastructure.Persistence
{
	public class EventStoreDbContext : DbContext
	{
		public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options) 
		{ 
		}

		public DbSet<StoredEvent> Events { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new StoredEventConfiguration());
			base.OnModelCreating(modelBuilder);

		}
	}

}