using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class PerfilPermissaoConfiguration : IEntityTypeConfiguration<PerfilPermissao>
    {
        public void Configure(EntityTypeBuilder<PerfilPermissao> builder)
        {
           
                builder.ToTable("PerfilPermissao");
                builder.HasKey(p => new { p.IdPerfil, p.IdPermissao });
                builder.Property(p => p.IdPerfil).HasColumnType("BIGINT").IsRequired();
                builder.Property(p => p.IdPermissao).HasColumnType("BIGINT").IsRequired();

                builder.HasOne(p => p.Perfil)
                .WithMany(p => p.PerfilPermissoes)
                .HasForeignKey(p => p.IdPerfil);

                builder.HasOne(p => p.Permissao)
                .WithMany(p => p.PerfilPermissoes)
                .HasForeignKey(p => p.IdPermissao);
           
        }
    }
}
