using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesTest.Fixtures
{
	public class PermissaoTestFixtures
	{
		#region Dependências
		private readonly Faker _faker;
		#endregion

		#region Construtor
		public PermissaoTestFixtures()
		{
			_faker = new Faker();
		}
		#endregion

		#region Faker Model
		public Permissao GerarPermissao()
		{
			//Arrange
			var id = _faker.UniqueIndex;			
			var descricao = _faker.Lorem.Paragraph();
			var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
			var criadoPor = _faker.Name.FirstName();
			var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
			var atualizadoPor = _faker.Name.FirstName();			

			var permissao = new Permissao( descricao, criadoPor )
			{
				Id = _faker.UniqueIndex,
				DataCriacao = _faker.Date.Past(yearsToGoBack: 100),
                DataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now),
                AtualizadoPor = _faker.Name.FirstName(),
            };

			return permissao;
		}

		public static Faker<Permissao> GerarPermissaoFaker()
		{
			var permissaoFaker = new Faker<Permissao>("pt_BR")
				.CustomInstantiator(f => new Permissao(
					descricao: f.Lorem.Paragraph(),
					criadoPor: f.Name.FirstName()
					))
				.RuleFor(e => e.Id, f => f.UniqueIndex)
				.RuleFor(e => e.DataCriacao, f => f.Date.Past(yearsToGoBack: 100))
				.RuleFor(e => e.DataAtualizacao, (f, e) => f.Date.Between(e.DataCriacao, DateTime.Now))
				.RuleFor(e => e.AtualizadoPor, f => f.Name.FirstName());

			return permissaoFaker;
		}
		#endregion

		#region TODO Faker DTOs
		#endregion

		#region TODO Faker Requests
		#endregion
	}
}
