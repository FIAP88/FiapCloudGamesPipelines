using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesTest.Fixtures
{
	public class AvaliacaoTestFixtures
	{
		#region Dependências
		private readonly Faker _faker;
		#endregion

		#region Construtor
		public AvaliacaoTestFixtures()
		{
			_faker = new Faker();
		}
		#endregion
		
		#region Faker Model
		public Avaliacao GerarAvaliacao()
		{			
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
                DataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now),
                AtualizadoPor = _faker.Name.FirstName(),
            };
            
            return avaliacao;
		}

		public static Faker<Avaliacao> GerarAvaliacaoFaker()
		{
			var avaliacaoFaker = new Faker<Avaliacao>("pt_BR")
				.CustomInstantiator(f => new Avaliacao(
					f.UniqueIndex,
					f.UniqueIndex,
					f.Random.Int(min: 0, max: 5),
					f.Name.JobDescriptor(),
					f.Name.FirstName()
					))
				.RuleFor(e => e.Id, f => f.UniqueIndex)
				.RuleFor(e => e.DataCriacao, f => f.Date.Past(yearsToGoBack: 100))
				.RuleFor(e => e.DataAtualizacao, (f, e) => f.Date.Between(e.DataCriacao, DateTime.Now))
				.RuleFor(e => e.AtualizadoPor, f => f.Name.FirstName());

			return avaliacaoFaker;
		}
		#endregion

		#region TODO Faker de DTOs

		#endregion

		#region TODO Faker de Requests

		#endregion

	}
}
