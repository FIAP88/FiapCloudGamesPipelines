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
        public DbSet<BibliotecaDoJogador> BibliotecasDoJogador { get; set; } = null!;
        public DbSet<Avaliacao> Avaliacoes { get; set; } = null!;
        public DbSet<Jogo> Jogos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<EmpresaFornecedora> EmpresasFornecedoras { get; set; } = null!;
        public DbSet<Log> Logs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
