using FiapCloudGamesAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesTest.Infra
{
	public static class HelperTests
	{		
		#region Context	
		public static AppDbContext GetInMemoryContext(string dbName = "Db_TechChallenge")
		{
			var builder = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(databaseName: dbName);
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
