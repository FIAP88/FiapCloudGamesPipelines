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
			var descricao = _faker.Name.JobDescriptor();			
			var criadoPor = _faker.Name.FirstName();
			var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);

            var categoria = new Categoria(descricao, criadoPor)
            {
                Id = _faker.UniqueIndex,
                DataCriacao = dataCriacao,
                AtualizadoPor = _faker.Name.FirstName(),
                DataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now),
            };

            return categoria;
		}
	}
}
