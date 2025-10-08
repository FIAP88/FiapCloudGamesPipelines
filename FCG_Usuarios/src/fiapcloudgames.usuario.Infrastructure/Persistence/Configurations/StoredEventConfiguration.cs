using fiapcloudgames.usuario.Infrastructure.EventStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// SqlServerDbContext //
namespace fiapcloudgames.usuario.Infrastructure.Persistence.Configurations
{
	public class StoredEventConfiguration : IEntityTypeConfiguration<StoredEvent>
	{
		public void Configure(EntityTypeBuilder<StoredEvent> builder)
		{
			builder.ToTable("Events");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.AggregateId)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(e => e.EventType)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(e => e.Payload)
				.IsRequired();

			builder.Property(e => e.Timestamp)
				.HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
