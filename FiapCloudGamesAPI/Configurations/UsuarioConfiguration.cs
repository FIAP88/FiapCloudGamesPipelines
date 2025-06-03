using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FiapCloudGamesAPI.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME2").IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Sobrenome).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.Apelido).HasColumnType("VARCHAR(50)");
            builder.HasIndex(p => p.Apelido).IsUnique();
            builder.Property(p => p.Email).HasColumnType("VARCHAR(150)").IsRequired();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(p => p.HashSenha).HasColumnType("VARCHAR(255)").IsRequired();
            builder.Property(p => p.DataNascimento).HasColumnType("DATETIME2");
            builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME2");
            builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");

            builder.Property(p => p.PerfilId).HasColumnType("BIGINT");

            builder.HasOne(p => p.Perfil)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(p => p.PerfilId);

            builder.HasMany(p => p.Avaliacoes)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.IdUsuario);

            
            builder.HasData(
                new Usuario("João", "Silva", "joaos", "joao@email.com", "7+D7gmaWMXRYtMBOLDAtRSgnqJoQ5H62L1setgRLRCx68knp71V1pdUZV6KfWoiT", DateTime.Parse("2000-01-01 00:00:00"), 1, "system") { Nome = "João", Id = 1, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Usuario("Gabriel", "Silva", "gabriel", "gabriel@email.com", "7+D7gmaWMXRYtMBOLDAtRSgnqJoQ5H62L1setgRLRCx68knp71V1pdUZV6KfWoiT", DateTime.Parse("2000-01-01 00:00:00"), 2, "system") { Nome = "Gabriel", Id = 2, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") }
            );
        }
    }
}
