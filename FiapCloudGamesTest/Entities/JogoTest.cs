using System;

namespace FiapCloudGamesTest.Entities
{
	public class JogoTest
	{
		private JogoTestFixtures _jogoTestFixtures;

		public JogoTest()
		{
			_jogoTestFixtures = new JogoTestFixtures();
		}

		#region Jogos           

		[Fact(DisplayName = "Validando a criação de Jogo, Jogo sem nome")]
		[Trait("Jogo", "Validando Jogos")]
		public void Add_JogoSemNome_DeveRetornarExcecao()
		{
			//Arrange

			//Act
			var ex = Assert.Throws<ArgumentException>(() =>
				_jogoTestFixtures.GerarJogoSemNome());

			//Assert
			Assert.Equal("Nome inválido", ex.Message);
		}
		#endregion
	}
}

