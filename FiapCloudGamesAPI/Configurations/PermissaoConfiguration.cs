using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class PermissaoConfiguration : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            
                builder.ToTable("Permissao");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
                builder.Property(p => p.Descricao).HasColumnType("VARCHAR(150)");
                builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
                builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)");
                builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
                builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");


            
        }
    }
}
