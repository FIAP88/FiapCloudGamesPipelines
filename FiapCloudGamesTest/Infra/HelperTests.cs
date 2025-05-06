using FiapCloudGamesAPI.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesTest.Infra
{
	internal class HelperTests
	{
		#region Context
		private readonly AppDbContext appDbContext;
		public HelperTests()
		{
			var builder = new DbContextOptionsBuilder<AppDbContext>();
			builder.UseInMemoryDatabase(databaseName: "Db_TechChallenge");

			var dbContextOptions = builder.Options;
			appDbContext = new AppDbContext(dbContextOptions);
			appDbContext.Database.EnsureDeleted();
			appDbContext.Database.EnsureCreated();
		}
		#endregion

		#region Repositórios
		//public UsuarioRepository GetInMemoryUsuarioRepository()			
		//{
		//	return new UsuarioRepository(appDbContext);
		//}

		//public JogoRepository GetInMemoryJogoRepository()
		//{
		//	return new JogoRepository(appDbContext);
		//}
		#region Generico
		//public BaseRepository<TEntity> GetInMemoryRepository<TEntity>()
		//	where TEntity : BaseEntity
		//{
		//	return new BaseRepository<TEntity>(appDbContext);
		//}
		#endregion
		#endregion

	}
}
