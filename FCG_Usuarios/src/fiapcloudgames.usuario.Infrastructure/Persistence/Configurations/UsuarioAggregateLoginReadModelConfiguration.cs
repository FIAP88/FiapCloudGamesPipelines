using fiapcloudgames.usuario.Infrastructure.Projections.ReadModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fiapcloudgames.usuario.Infrastructure.Persistence.Configurations
{
	public class UsuarioAggregateLoginReadModelConfiguration : IEntityTypeConfiguration<UsuarioAggregateLoginReadModel>
	{
		public void Configure(EntityTypeBuilder<UsuarioAggregateLoginReadModel> builder)
		{
            // Nome da tabela
            builder.ToTable("UsuarioLogin");

            builder.HasKey(u => u.UsuarioId);

            builder.Property(u => u.UsuarioId)
                   .IsRequired()
                   .HasMaxLength(36); // se for GUID como string

            builder.Property(u => u.PrimeiroNome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Sobrenome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Apelido)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(u => u.HashSenha)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

        }
	}
}
