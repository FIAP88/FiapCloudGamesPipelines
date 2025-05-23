using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class EmpresaFornecedoraConfiguration : IEntityTypeConfiguration<EmpresaFornecedora>
    {
        public void Configure(EntityTypeBuilder<EmpresaFornecedora> builder)
        {
         
                builder.ToTable("EmpresaFornecedora");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
                builder.Property(p => p.Nome).HasColumnType("VARCHAR(150)").IsRequired();
                builder.Property(p => p.CNPJ).HasColumnType("CHAR(50)").IsRequired();
                builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
                builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)");
                builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
                builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");

         
        }
    }
}
