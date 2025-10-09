using fiapcloudgames.usuario.Infrastructure.Persistence.Configurations;
using fiapcloudgames.usuario.Infrastructure.Projections.ReadModel;
using Microsoft.EntityFrameworkCore;

namespace fiapcloudgames.usuario.Infrastructure.Persistence
{
	public class ReadModelDbContext : DbContext
	{
		public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options) : base(options)
		{

		}

		public DbSet<UsuarioAggregateReadModel> Usuarios { get; set; }
		
        public DbSet<UsuarioAggregateLoginReadModel> UsuariosLogin { get; set; } // ReadModel/projecao dedicada/otimizada a Autenticacao

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UsuarioAggregateReadModelConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioAggregateLoginReadModelConfiguration());
            base.OnModelCreating(modelBuilder);
			
		}
	}
}
