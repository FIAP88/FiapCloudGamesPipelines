using Bogus;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesTest.Fixtures
{
	public class CategoriaTestFixtures
	{
		private readonly Faker _faker;
		public CategoriaTestFixtures()
		{
			_faker = new Faker();
		}
		public Categoria GerarCategoria()
		{
			//Arrange
			var id = _faker.UniqueIndex;			
			var descricao = _faker.Name.JobDescriptor;			
			var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
			var criadoPor = _faker.Name.FirstName;
			var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
			var atualizadoPor = _faker.Name.FirstName;			

			var categoria = new Categoria(id, descricao, dataCriacao,
				criadoPor, dataAtualizacao, atualizadoPor);

			return categoria;
		}
	}
}
