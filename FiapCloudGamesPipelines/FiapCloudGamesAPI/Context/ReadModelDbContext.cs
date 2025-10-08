using FiapCloudGamesAPI.Configurations;
using FiapCloudGamesAPI.EventStore.Projection.Model;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGamesAPI.Context
{
	public class ReadModelDbContext : DbContext
	{
		public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options) : base(options)
		{

		}

		public DbSet<UsuarioAggregateReadModel> Usuarios { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UsuarioAggregateReadModelConfiguration());
			base.OnModelCreating(modelBuilder);
			
		}
	}
}
