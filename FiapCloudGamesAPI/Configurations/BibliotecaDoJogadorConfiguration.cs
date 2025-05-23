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
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
                builder.Property(p => p.IdJogo).HasColumnType("BIGINT").IsRequired();
                builder.Property(p => p.IdUsuario).HasColumnType("BIGINT").IsRequired();
                builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();

                builder.HasOne(p => p.Usuario)
                .WithOne(p => p.Biblioteca)
                .HasForeignKey<BibliotecaDoJogador>(p => p.IdUsuario);

            builder.HasMany(p => p.Jogos)
             .WithMany(p => p.Bibliotecas);
                 
           
        }
    }
}
