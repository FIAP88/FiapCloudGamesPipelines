using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGamesAPI.Configurations
{
    public class JogoUsuarioConfiguration : IEntityTypeConfiguration<JogoUsuario>
    {
        public void Configure(EntityTypeBuilder<JogoUsuario> builder)
        {

            builder.ToTable("JogoUsuario");

            builder.HasKey(p => new { p.UsuarioId, p.JogoId });
            builder.Property(p => p.UsuarioId).HasColumnType("BIGINT").IsRequired();
            builder.Property(p => p.JogoId).HasColumnType("BIGINT").IsRequired();

            builder.HasOne(ju => ju.Usuario)
                .WithMany(u => u.JogosDoUsuario)
                .HasForeignKey(ju => ju.UsuarioId);

            builder.HasOne(ju => ju.Jogo)
                .WithMany(u => u.UsuariosDoJogo) 
                .HasForeignKey(ju => ju.JogoId);
        }
    }
}
