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

		#region Usu�rio	

		[Fact(DisplayName = "Validando a cria��o de Usuario, com Senha inv�lida")]
		[Trait("Usu�rio", "Validando Usu�rios")]
		public void Add_UsuarioComSenhaInvalida_DeveRetornarExcecao()
		{
			//Arrange		

			//Act
			var ex = Assert.Throws<ArgumentException>(() =>
				_usuarioTestFixtures.GerarUsuarioComSenhaInvalida());

			//Assert
			Assert.Equal("Senha inv�lida", ex.Message);
		}


		[Fact(DisplayName = "Validando a cria��o de Usuario, com Email inv�lido")]
		[Trait("Usu�rio", "Validando Usu�rios")]
		public void Add_UsuarioComEmailInvalido_DeveRetornarExcecao()
		{
			//Arrange

			//Act
			var ex = Assert.Throws<ArgumentException>(() =>
				_usuarioTestFixtures.GerarUsuarioComEmailInvalido());

			//Assert
			Assert.Equal("Email inv�lido", ex.Message);
		}
		#endregion

	}

}
