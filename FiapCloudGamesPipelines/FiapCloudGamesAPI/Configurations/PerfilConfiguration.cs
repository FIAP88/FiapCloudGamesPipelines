using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGamesAPI.Configurations
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
           
            builder.ToTable("Perfil");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(p => p.Descricao).HasColumnType("VARCHAR(150)");
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME2").IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME2");
            builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");

            builder.HasData(
                new Perfil("Administrador", "system") { Id = 1, CriadoPor = "system", DataCriacao = DateTime.Parse("2025-01-01 00:00:00")  },
                new Perfil("Jogador", "system") { Id = 2, CriadoPor = "system", DataCriacao = DateTime.Parse("2025-01-01 00:00:00") }
            );
        }
    }
}
