using Bogus.Extensions.Brazil;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesTest.Fixtures
{
	public class EmpresaFornecedoraTestFixtures
	{
		#region Dependências
		private readonly Faker _faker;
		#endregion

		#region Construtor
		public EmpresaFornecedoraTestFixtures()
		{
			_faker = new Faker();
		}
		#endregion

		#region Faker Model
		public EmpresaFornecedora GerarEmpresaFornecedora()
		{
			//Arrange
			var id = _faker.UniqueIndex;
			var nome = _faker.Company.CompanyName();
			var cnpj = _faker.Company.Cnpj();			
			var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
			var criadoPor = _faker.Name.FirstName();
			var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
			var atualizadoPor = _faker.Name.FirstName();

			var empresaFornecedora = new EmpresaFornecedora(nome, cnpj, criadoPor)
			{
				Id = id,
				DataCriacao = dataCriacao,
				DataAtualizacao = dataAtualizacao,
				AtualizadoPor = atualizadoPor,
			}; 

			return empresaFornecedora;
		}

		public static Faker<EmpresaFornecedora> GerarEmpresaFornecedoraFaker()
		{
			var empresaFornecedoraFaker = new Faker<EmpresaFornecedora>("pt_BR")
				.CustomInstantiator(f => new EmpresaFornecedora(
					f.Company.CompanyName(),
					f.Company.Cnpj(),
					f.Name.FirstName()
					))
				.RuleFor(e => e.Id, f => f.UniqueIndex)
				.RuleFor(e => e.DataCriacao, f => f.Date.Past(yearsToGoBack: 100))
				.RuleFor(e => e.DataAtualizacao, (f, e) => f.Date.Between(e.DataCriacao, DateTime.Now))
				.RuleFor(e => e.AtualizadoPor, f => f.Name.FirstName());

			return empresaFornecedoraFaker;
		}
		#endregion

		#region TODO Faker DTOs
		#endregion

		#region TODO Faker Requests
		#endregion
	}
}
