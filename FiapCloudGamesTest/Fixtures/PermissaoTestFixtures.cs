using Bogus;
using Bogus.Extensions;
using FiapCloudGamesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesTest.Fixtures
{
	public class PermissaoTestFixtures
	{
		private readonly Faker _faker;
		public PermissaoTestFixtures()
		{
			_faker = new Faker();
		}

		public Permissao GerarPermissao()
		{
			//Arrange
			var id = _faker.UniqueIndex;
			var nome = _faker.Internet.DomainName();
			var descricao = _faker.Lorem.Paragraph();
			var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
			var criadoPor = _faker.Name.FirstName;
			var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
			var atualizadoPor = _faker.Name.FirstName;			

			var permissao = new Permissao(id, nome, descricao, dataCriacao,
				criadoPor, dataAtualizacao, atualizadoPor);

			return permissao;
		}

	}
}
