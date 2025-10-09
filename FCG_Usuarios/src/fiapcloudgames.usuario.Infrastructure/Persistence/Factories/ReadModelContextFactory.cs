using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace fiapcloudgames.usuario.Infrastructure.Persistence.Factories
{
	public class ReadModelDbContextFactory : IDesignTimeDbContextFactory<ReadModelDbContext>
	{
		public ReadModelDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ReadModelDbContext>();
			
			optionsBuilder.UseSqlServer(
				"Server=localhost\\SQLExpress;Database=ReadModelDb;Trusted_Connection=true;TrustServerCertificate=true"
			);

			return new ReadModelDbContext(optionsBuilder.Options);
		}
	}
}
