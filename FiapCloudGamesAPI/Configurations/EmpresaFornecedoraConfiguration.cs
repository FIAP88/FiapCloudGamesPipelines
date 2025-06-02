using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
            builder.Property(p => p.CNPJ).HasColumnType("VARCHAR(50)").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME2").IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME2");
            builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");
            
            
            builder.HasData(

                new EmpresaFornecedora("GameX Studios", "12345678000190", "system") { Id = 1, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new EmpresaFornecedora("AlphaGames Ltda", "98765432000191", "system") { Id = 2, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") } 
            );
        }
    }
}
