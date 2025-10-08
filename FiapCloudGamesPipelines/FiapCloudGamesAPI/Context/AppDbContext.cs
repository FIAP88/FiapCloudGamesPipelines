using FiapCloudGamesAPI.Configurations;
using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGamesAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Perfil> Perfis { get; set; } = null!;
        public DbSet<Permissao> Permissoes { get; set; } = null!;
        public DbSet<PerfilPermissao> PerfisPermissoes { get; set; } = null!;
        public DbSet<Avaliacao> Avaliacoes { get; set; } = null!;
        public DbSet<Jogo> Jogos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<EmpresaFornecedora> EmpresasFornecedoras { get; set; } = null!;
        public DbSet<Log> Logs { get; set; } = null!;
        public DbSet<JogoUsuario> JogosUsuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Removi a o ApplyConfigurationsFromAssembly, pois é necessario garantiar uma seguencia na inserção dos dados das tabelas.
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new PerfilConfiguration());
            modelBuilder.ApplyConfiguration(new JogoUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new PermissaoConfiguration());
            modelBuilder.ApplyConfiguration(new PerfilPermissaoConfiguration());
            modelBuilder.ApplyConfiguration(new AvaliacaoConfiguration());
            modelBuilder.ApplyConfiguration(new JogoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new EmpresaFornecedoraConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
            
            base.OnModelCreating(modelBuilder);

        }
    }
}
