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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(e => {
                e.ToTable("Usuario");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                e.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
                e.Property(p => p.Nome).HasColumnType("VARCHAR(100)").IsRequired();
                e.Property(p => p.Sobrenome).HasColumnType("VARCHAR(100)");
                e.Property(p => p.Apelido).HasColumnType("VARCHAR(50)");
                e.Property(p => p.Email).HasColumnType("VARCHAR(150)").IsRequired();
                e.HasIndex(e => e.Email).IsUnique();
                e.Property(p => p.HashSenha).HasColumnType("VARCHAR(255)").IsRequired();
                e.Property(p => p.DataNascimento).HasColumnType("INT");
                e.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)");
                e.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
                e.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");

                e.Property(p => p.PerfilId).HasColumnType("INT");

                e.HasOne(p => p.Perfil)
                .WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(p => p.PerfilId);
            });

            modelBuilder.Entity<Perfil>(e => {
                e.ToTable("Perfil");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                e.Property(p => p.Descricao).HasColumnType("VARCHAR(150)");

            });

            modelBuilder.Entity<Permissao>(e => {
                e.ToTable("Permissao");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                e.Property(p => p.Descricao).HasColumnType("VARCHAR(150)");
                e.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
                e.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)");
                e.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
                e.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");


            });

            modelBuilder.Entity<PerfilPermissao>(e => {
                e.ToTable("PerfilPermissao");
                e.Property(p => p.IdPerfil).HasColumnType("INT").IsRequired();
                e.Property(p => p.IdPermissao).HasColumnType("INT").IsRequired();

                e.HasOne(p => p.Perfil)
                .WithMany(p => p.PerfilPermissoes)
                .HasForeignKey(p => p.IdPerfil);

                e.HasOne(p => p.Permissao)
                .WithMany(p => p.PerfilPermissoes)
                .HasForeignKey(p => p.IdPermissao);
            });

            modelBuilder.Entity<BibliotecaDoJogador>(e => {
                e.ToTable("BibliotecaDoJogador");
                e.Property(p => p.IdJogo).HasColumnType("INT").IsRequired();
                e.Property(p => p.IdUsuario).HasColumnType("INT").IsRequired();
                e.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();

                e.HasOne(p => p.Usuario)
                .WithMany(p => p.Biblioteca)
                .HasForeignKey(p => p.IdUsuario);

                e.HasOne(p => p.Jogo)
                 .WithMany(p => p.Bibliotecas)
                 .HasForeignKey(p => p.IdJogo);
            });

            modelBuilder.Entity<Avaliacao>(e => {
                e.ToTable("Avaliacao");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                e.Property(p => p.IdJogo).HasColumnType("INT").IsRequired();
                e.Property(p => p.IdUsuario).HasColumnType("INT").IsRequired();
                e.Property(p => p.Nota).HasColumnType("INT");
                e.Property(p => p.Comentario).HasColumnType("VARCHAR(MAX)");
                e.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();

                e.HasOne(p => p.Usuario)
                .WithMany(p => p.Avaliacoes)
                .HasForeignKey(p => p.IdUsuario);

                e.HasOne(p => p.Jogo)
                .WithMany(p => p.Avaliacoes)
                .HasForeignKey(p => p.IdJogo);
            });

            modelBuilder.Entity<Jogo>(e => {
                e.ToTable("Jogo");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                e.Property(p => p.Nome).HasColumnType("VARCHAR(150)").IsRequired();
                e.Property(p => p.Descricao).HasColumnType("VARCHAR(1000)");
                e.Property(p => p.Tamanho).HasColumnType("DECIMAL(10,2)");
                e.Property(p => p.Preco).HasColumnType("INT");
                e.Property(p => p.Descricao).HasColumnType("VARCHAR(150)");
                e.Property(p => p.IdCategoria).HasColumnType("INT");
                e.Property(p => p.IdadeMinima).HasColumnType("INT");
                e.Property(p => p.Ativo).HasColumnType("BIT").HasDefaultValue(true); ;
                e.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
                e.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)");
                e.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
                e.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");
                e.Property(p => p.IdFornecedor).HasColumnType("INT").IsRequired();

                e.HasOne(p => p.Categoria)
                 .WithOne(p => p.Jogo)
                 .HasForeignKey<Jogo>(p => p.IdCategoria);

                e.HasOne(p => p.EmpresaFornecedora)
                 .WithOne(p => p.Jogo)
                 .HasForeignKey<Jogo>(p => p.IdFornecedor);
            });

            modelBuilder.Entity<Categoria>(e => {
                e.ToTable("Categoria");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                e.Property(p => p.Descricao).HasColumnType("VARCHAR(100)").IsRequired();
                e.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
                e.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)");
                e.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
                e.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");
            });

            modelBuilder.Entity<EmpresaFornecedora>(e => {
                e.ToTable("EmpresaFornecedora");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                e.Property(p => p.Nome).HasColumnType("VARCHAR(150)").IsRequired();
                e.Property(p => p.CNPJ).HasColumnType("CHAR(50)").IsRequired();
                e.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
                e.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)");
                e.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
                e.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");

            });

        }
    }
}
