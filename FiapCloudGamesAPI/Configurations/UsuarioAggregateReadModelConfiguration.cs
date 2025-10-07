using FiapCloudGamesAPI.EventStore.Projection.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGamesAPI.Configurations
{
	public class UsuarioAggregateReadModelConfiguration : IEntityTypeConfiguration<UsuarioAggregateReadModel>
	{
		public void Configure(EntityTypeBuilder<UsuarioAggregateReadModel> builder)
		{
			builder.ToTable("Usuarios");
            builder.HasKey(e => e.AggregateId); 
			builder.Property(e => e.AggregateId)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(e => e.Nome)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(e => e.Email)
				.IsRequired();

		}
	}
}
