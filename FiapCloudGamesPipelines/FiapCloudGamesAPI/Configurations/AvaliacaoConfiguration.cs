using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
          
            builder.ToTable("Avaliacao");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(p => p.IdJogo).HasColumnType("BIGINT").IsRequired();
            builder.Property(p => p.IdUsuario).HasColumnType("BIGINT").IsRequired();
            builder.Property(p => p.Nota).HasColumnType("INT");
            builder.Property(p => p.Comentario).HasColumnType("VARCHAR(MAX)");
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME2").IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME2");
            builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");

            builder.HasOne(p => p.Usuario)
                .WithMany(p => p.Avaliacoes)
                .HasForeignKey(p => p.IdUsuario);

                builder.HasOne(p => p.Jogo)
                .WithMany(p => p.Avaliacoes)
                .HasForeignKey(p => p.IdJogo);
            
        }
    }
}
