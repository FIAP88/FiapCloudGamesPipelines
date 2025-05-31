using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class BibliotecaDoJogadorConfiguration : IEntityTypeConfiguration<BibliotecaDoJogador>
    {
        public void Configure(EntityTypeBuilder<BibliotecaDoJogador> builder)
        {
          
            builder.ToTable("BibliotecaDoJogador");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(p => p.IdJogo).HasColumnType("BIGINT").IsRequired();
            builder.Property(p => p.IdUsuario).HasColumnType("BIGINT").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME2").IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME2");
            builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");

            builder.HasOne(p => p.Usuario)
                .WithOne(p => p.Biblioteca)
                .HasForeignKey<BibliotecaDoJogador>(p => p.IdUsuario);

            builder.HasMany(p => p.Jogos)
             .WithMany(p => p.Bibliotecas);
                 
           
        }
    }
}
