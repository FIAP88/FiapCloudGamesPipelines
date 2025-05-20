using Bogus;
using FiapCloudGamesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesTest.Fixtures
{
	public class AvaliacaoTestFixtures
	{
		private readonly Faker _faker;
		public AvaliacaoTestFixtures()
		{
			_faker = new Faker();
		}
		public Avaliacao GerarAvaliacao()
		{
			//Arrange
			var id = _faker.UniqueIndex;
			var idUsuario = _faker.UniqueIndex;
			var idJogo = _faker.UniqueIndex;
			var nota = _faker.Random.Int(min: 0, max: 5);
			var comentario =_faker.Name.JobDescriptor;
			var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);

			var avaliacao = new Avaliacao(id, idUsuario, idJogo, nota,
				comentario, dataCriacao);

			return avaliacao;
		}
	}
}
