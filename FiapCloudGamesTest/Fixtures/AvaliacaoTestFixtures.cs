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
            var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
            
			var idUsuario = _faker.UniqueIndex;
			var idJogo = _faker.UniqueIndex;
			var nota = _faker.Random.Int(min: 0, max: 5);
			var comentario =_faker.Name.JobDescriptor();
			var criadoPor = _faker.Name.FirstName();

            var avaliacao = new Avaliacao(idUsuario, idJogo, nota, comentario, criadoPor)
			{
				Id = _faker.UniqueIndex,
				DataCriacao  = dataCriacao,
                AtualizadoPor = _faker.Name.FirstName(),
                DataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now),
            };
            

            return avaliacao;
		}
	}
}
