using Bogus;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesTest.Fixtures
{
	public class PerfilTestFixture
	{
		private readonly Faker _faker;
		public PerfilTestFixture()
		{
			_faker = new Faker();
		}

		public Perfil GerarPerfil()
		{
			//Arrange
			var id = _faker.UniqueIndex;			
			var descricao = _faker.Name.JobDescriptor();

			var perfil = new Perfil(id, descricao);

			return perfil;
		}

		public static Faker<Perfil> GerarPerfilFaker()
		{
			var perfilFaker = new Faker<Perfil>("pt_BR")
				.CustomInstantiator(f => new Perfil(
					f.UniqueIndex,
					f.Name.JobDescriptor()
					))
				.RuleFor(e => e.Id, f => f.UniqueIndex)
				.RuleFor(e => e.Descricao, f => f.Name.JobDescriptor());

			return perfilFaker;
		}

	}
}
