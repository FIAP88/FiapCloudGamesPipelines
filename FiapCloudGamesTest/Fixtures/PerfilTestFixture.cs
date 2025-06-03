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
			var criadoPor = _faker.Name.FirstName();
			var perfil = new Perfil(criadoPor, descricao);

			return perfil;
		}

		public static Faker<Perfil> GerarPerfilFaker()
		{
			var perfilFaker = new Faker<Perfil>("pt_BR")
				.CustomInstantiator(f => new Perfil(
					f.Name.JobDescriptor(),
					f.Name.FirstName()
					))
				.RuleFor(e => e.Id, f => f.UniqueIndex);				

			return perfilFaker;
		}
		#endregion

		#region TODO Faker DTOs
		#endregion

		#region TODO Faker Requests
		#endregion
	}
}
