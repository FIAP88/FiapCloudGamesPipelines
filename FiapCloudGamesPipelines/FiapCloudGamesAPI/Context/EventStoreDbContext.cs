using FiapCloudGamesAPI.Configurations;
using Microsoft.EntityFrameworkCore;

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