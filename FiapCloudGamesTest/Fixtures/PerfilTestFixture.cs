using Bogus;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesTest.Fixtures
{
	public class PerfilTestFixture
	{
		#region Dependências
		private readonly Faker _faker;
		#endregion

		#region Construtor
		public PerfilTestFixture()
		{
			_faker = new Faker();
		}
		#endregion

		#region Faker Model
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
		#endregion

		#region TODO Faker DTOs
		#endregion

		#region TODO Faker Requests
		#endregion
	}
}
