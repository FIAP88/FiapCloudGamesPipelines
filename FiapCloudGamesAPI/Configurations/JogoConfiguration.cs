using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
           
                builder.ToTable("Jogo");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
                builder.Property(p => p.Nome).HasColumnType("VARCHAR(150)").IsRequired();
                builder.Property(p => p.Descricao).HasColumnType("VARCHAR(1000)");
                builder.Property(p => p.Tamanho).HasColumnType("DECIMAL(10,2)");
                builder.Property(p => p.Preco).HasColumnType("INT");
                builder.Property(p => p.Descricao).HasColumnType("VARCHAR(150)");
                builder.Property(p => p.IdCategoria).HasColumnType("BIGINT");
                builder.Property(p => p.IdadeMinima).HasColumnType("INT");
                builder.Property(p => p.Ativo).HasColumnType("BIT").HasDefaultValue(true); ;
                builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
                builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)");
                builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME");
                builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");
                builder.Property(p => p.IdFornecedor).HasColumnType("BIGINT").IsRequired();

                builder.HasOne(p => p.Categoria)
                 .WithOne(p => p.Jogo)
                 .HasForeignKey<Jogo>(p => p.IdCategoria);

                builder.HasOne(p => p.EmpresaFornecedora)
                 .WithOne(p => p.Jogo)
                 .HasForeignKey<Jogo>(p => p.IdFornecedor);
           
        }
    }
}
