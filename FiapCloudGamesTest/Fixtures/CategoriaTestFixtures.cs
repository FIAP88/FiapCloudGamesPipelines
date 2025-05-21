using Bogus;
using Bogus.Extensions.Brazil;
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
                DataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now),
                AtualizadoPor = _faker.Name.FirstName(),
            };

            return categoria;
		}

		public Faker<Categoria> GerarCategoriaFaker()
		{
			var categoriaFakerFactory = new Faker<Categoria>("pt_BR")
				.CustomInstantiator(f => new Categoria(
					f.Name.JobDescriptor(),
					f.Name.FirstName()					
					))
				.RuleFor(e => e.Id, f => f.UniqueIndex)
				.RuleFor(e => e.DataCriacao, f => f.Date.Past(yearsToGoBack: 100))
				.RuleFor(e => e.DataAtualizacao, (f, e) => f.Date.Between(e.DataCriacao, DateTime.Now))
				.RuleFor(e => e.AtualizadoPor, f => f.Name.FirstName());

			return categoriaFakerFactory;
		}

	}
}
