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
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Sobrenome).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.Apelido).HasColumnType("VARCHAR(50)");
            builder.Property(p => p.Email).HasColumnType("VARCHAR(150)").IsRequired();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(p => p.HashSenha).HasColumnType("VARCHAR(255)").IsRequired();
            builder.Property(p => p.DataNascimento).HasColumnType("DATETIME");
            builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
            builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");

            builder.Property(p => p.PerfilId).HasColumnType("BIGINT");

            builder.HasOne(p => p.Perfil)
               .WithMany(p => p.Usuarios)
               .HasForeignKey(p => p.PerfilId);

        }
    }
}
