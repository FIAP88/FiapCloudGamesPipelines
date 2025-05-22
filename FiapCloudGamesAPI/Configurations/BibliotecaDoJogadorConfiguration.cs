using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class BibliotecaDoJogadorConfiguration : IEntityTypeConfiguration<BibliotecaDoJogador>
    {
        public void Configure(EntityTypeBuilder<BibliotecaDoJogador> builder)
        {
          
                builder.ToTable("BibliotecaDoJogador");
                builder.Property(p => p.IdJogo).HasColumnType("INT").IsRequired();
                builder.Property(p => p.IdUsuario).HasColumnType("INT").IsRequired();
                builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();

                builder.HasOne(p => p.Usuario)
                .WithMany(p => p.Biblioteca)
                .HasForeignKey(p => p.IdUsuario);

                builder.HasOne(p => p.Jogo)
                 .WithMany(p => p.Bibliotecas)
                 .HasForeignKey(p => p.IdJogo);
           
        }
    }
}
