using System;
using FiapCloudGamesAPI.Models;

namespace FiapCloudGamesTest.Entities
{ 
	public class UsuarioTest
	{

		private UsuarioTestFixtures _usuarioTestFixtures;

		public UsuarioTest()
		{
			_usuarioTestFixtures = new UsuarioTestFixtures();
		}

		#region Usuário	

		[Fact(DisplayName = "Validando a criação de Usuario, com Senha inválida")]
		[Trait("Usuário", "Validando Usuários")]
		public void Add_UsuarioComSenhaInvalida_DeveRetornarExcecao()
		{
			//Arrange		

			//Act
			var ex = Assert.Throws<ArgumentException>(() =>
				_usuarioTestFixtures.GerarUsuarioComSenhaInvalida());

			//Assert
			Assert.Equal("Senha inválida", ex.Message);
		}


		[Fact(DisplayName = "Validando a criação de Usuario, com Email inválido")]
		[Trait("Usuário", "Validando Usuários")]
		public void Add_UsuarioComEmailInvalido_DeveRetornarExcecao()
		{
			//Arrange

			//Act
			var ex = Assert.Throws<ArgumentException>(() =>
				_usuarioTestFixtures.GerarUsuarioComEmailInvalido());

			//Assert
			Assert.Equal("Email inválido", ex.Message);
		}
		#endregion

	}

}
