using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
           
         builder.ToTable("Perfil");
         builder.HasKey(p => p.Id);
         builder.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
         builder.Property(p => p.Descricao).HasColumnType("VARCHAR(150)");

          
        }
    }
}
