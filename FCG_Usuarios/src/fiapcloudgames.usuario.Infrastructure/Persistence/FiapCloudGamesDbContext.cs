using fiapcloudgames.usuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace fiapcloudgames.usuario.Infrastructure.Persistence
{
    public class FiapCloudGamesDbContext : DbContext
    {
        public FiapCloudGamesDbContext(DbContextOptions<FiapCloudGamesDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set default schema
            //modelBuilder.HasDefaultSchema("DB_FiapCloudGamesUsuario");

            // Apply all entities configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FiapCloudGamesDbContext).Assembly);
        }
    }
}
