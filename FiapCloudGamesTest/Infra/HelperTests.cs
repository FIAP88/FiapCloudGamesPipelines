using FiapCloudGamesAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGamesTest.Infra
{
	public static class HelperTests
	{
        #region Context	
        public static AppDbContext GetInMemoryContext()
        {
            var uniqueDbName = $"Db_TechChallenge_{Guid.NewGuid()}";
            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: uniqueDbName);
            var dbContextOptions = builder.Options;
            var appDbContext = new AppDbContext(dbContextOptions);

            appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();

            return appDbContext;
        }		
		#endregion

		#region Repositórios

		//public UsuarioRepository GetInMemoryUsuarioRepository()			
		//{
		//	return new UsuarioRepository(appDbContext);
		//}
		
		//public BaseRepository<TEntity> GetInMemoryRepository<TEntity>()
		//	where TEntity : BaseEntity
		//{
		//	return new BaseRepository<TEntity>(appDbContext);
		//}

		#endregion

	}
}
