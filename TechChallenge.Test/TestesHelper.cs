using Microsoft.EntityFrameworkCore;

namespace TechChallenge.Test
{
	public class TestesHelper
	{
		#region Context
		private readonly MySqlServerContext mySqlContext;
		public TestesHelper()
		{
			var builder = new DbContextOptionsBuilder<MySqlServerContext>();
			builder.UseInMemoryDatabase(databaseName: "Db_TechChallenge");

			var dbContextOptions = builder.Options;
			mySqlContext = new MySqlServerContext(dbContextOptions);			
			mySqlContext.Database.EnsureDeleted();
			mySqlContext.Database.EnsureCreated();
		}
		#endregion

		#region Repositórios
		public BaseRepository<TEntity> GetInMemoryRepository<TEntity>()
			where TEntity : BaseEntity
		{
			return new BaseRepository<TEntity>(mySqlContext);
		}
		#endregion

	}
}
