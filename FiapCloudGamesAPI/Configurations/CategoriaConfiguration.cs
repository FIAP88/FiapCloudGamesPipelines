using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
           
            builder.ToTable("Categoria");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(p => p.Descricao).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
            builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");
            
        }
    }
}
