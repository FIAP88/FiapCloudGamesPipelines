using Bogus;
using Bogus.Extensions.Brazil;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesTest.Fixtures
{
	public class EmpresaFornecedoraTestFixtures
	{
		private readonly Faker _faker;
		public EmpresaFornecedoraTestFixtures()
		{
			_faker = new Faker();
		}

		public EmpresaFornecedora GerarEmpresaFornecedora()
		{
			//Arrange
			var id = _faker.UniqueIndex;
			var nome = _faker.Company.CompanyName();
			var cnpj = _faker.Company.Cnpj;			
			var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
			var criadoPor = _faker.Name.FirstName;
			var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
			var atualizadoPor = _faker.Name.FirstName;			

			var empresaFornecedora = new EmpresaFornecedora(id, nome, cnpj, dataCriacao,
				criadoPor, dataAtualizacao, atualizadoPor);

			return empresaFornecedora;
		}
	}
}
