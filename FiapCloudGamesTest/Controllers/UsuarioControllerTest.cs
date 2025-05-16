using FiapCloudGamesAPI.Controllers;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesAPI.Services;
using FiapCloudGamesTest.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
//https://learn.microsoft.com/pt-br/aspnet/core/mvc/controllers/testing?view=aspnetcore-9.0
//https://www.macoratti.net/15/09/mvc_mdlst1.htm

namespace FiapCloudGamesTest.Controllers
{
	public class UsuarioControllerTest
	{
		#region Depedências
		//private Mock<ILogger<JogoController>> _logger;
		//private UsuariosController _controller;
		private Mock<IUsuarioService> _serviceMock;
		#endregion

		#region Construtor
		public UsuarioControllerTest()
		{
			_serviceMock = new Mock<IUsuarioService>();
			//_controller = new UsuariosController(HelperTests.GetInMemoryContext(), _serviceMock.Object);
		}
		#endregion

		#region Requests
		[Fact(DisplayName = "GetUsuarios deve retornar todos os usuários")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Get_RetornaTodosUsuarios()
		{
			//Arrange
			var context = HelperTests.GetInMemoryContext();
			context.AddRange(
				new Usuario { UserId = 1, Name = "Leo" },
				new Usuario { UserId = 2, Name = "Gabriel" }
			);
			await context.SaveChangesAsync();

			var controller = new UsuariosController(context, _serviceMock.Object);			

			//Act
			var result = await controller.GetUsuarios();

			//Assert
			var actionResult = Assert.IsType<ActionResult<IEnumerable<Usuario>>>(result);
			var users = Assert.IsAssignableFrom<IEnumerable<Usuario>>(actionResult.Value);
			Assert.Equal(2, users.Count());
		}

		[Fact(DisplayName = "GetUsuario deve retornar um usuário existente")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Get_RetornaUsuario()
		{
			//Arrange
			var context = HelperTests.GetInMemoryContext();
			context.Add(
				new Usuario { UserId = 1, Name = "Leo" }				
			);
			await context.SaveChangesAsync();

			var controller = new UsuariosController(context, _serviceMock.Object);
			
			//Act
			var result = await controller.GetUsuario(1);

			//Assert
			var actionResult = Assert.IsType<ActionResult<Usuario>>(result);
			var foundUser = Assert.IsType<Usuario>(actionResult.Value);
			Assert.Equal("Leo", foundUser.Name);
		}

		[Fact(DisplayName = "PostUsuario deve criar um novo usuário")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Post_CriaNovoUsuario()
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _serviceMock.Object);			
			var novoUsuario = new Usuario { UserId = 3, Name = "Fernanda" };

			// Act
			var result = await controller.PostUsuario(novoUsuario);

			// Assert
			var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
			var createdUser = Assert.IsType<Usuario>(createdAt.Value);
			Assert.Equal("Fernanda", createdUser.Name);
		}

		[Fact(DisplayName = "DeleteUsuario deve remover usuário existente")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Delete_RemoveUsuarioQuandoEncontrado()
		{
			//Arrange
			var context = HelperTests.GetInMemoryContext();
			context.Add(
				new Usuario { UserId = 4, Name = "Pedro" }
			);
			await context.SaveChangesAsync();

			var controller = new UsuariosController(context, _serviceMock.Object);

			//Act
			var result = await controller.DeleteUsuario(4);

			//Assert
			Assert.IsType<NoContentResult>(result);
			Assert.False(context.Usuarios.Any(u => u.UserId == 4));
		}

		[Fact(DisplayName = "PutUsuario deve atualizar usuário quando IDs forem iguais")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Put_AtualizaUsuarioComMesmoID()
		{
			//Arrange
			var usuario = new Usuario { UserId = 5, Name = "Ana" };
			var context = HelperTests.GetInMemoryContext();
			context.Add(
				usuario
			);
			await context.SaveChangesAsync();
			usuario.Name = "Ana Atualizada";

			var controller = new UsuariosController(context, _serviceMock.Object);

			// Act
			var result = await controller.PutUsuario(5, usuario);

			// Assert
			Assert.IsType<NoContentResult>(result);
			Assert.Equal("Ana Atualizada", context.Usuarios.Find(5)?.Name);
		}

		[Fact(DisplayName = "GetUsuario deve retornar NotFound para usuário inexistente")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task GetUsuario_RetornaNotFound()
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _serviceMock.Object);
			// Act
			var result = await controller.GetUsuario(999);
			// Assert
			Assert.IsType<NotFoundResult>(result.Result);
		}

		[Fact(DisplayName = "PutUsuario deve retornar BadRequest quando IDs forem diferentes")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task PutUsuario_ReturnaBadRequest()
		{
			// Arrange
			var usuario = new Usuario { UserId = 10, Name = "Carlos" };
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _serviceMock.Object);
			// Act
			var result = await controller.PutUsuario(99, usuario);
			// Assert
			Assert.IsType<BadRequestResult>(result);
		}
		#endregion

		#region ViewsTests

		#endregion
	}
}