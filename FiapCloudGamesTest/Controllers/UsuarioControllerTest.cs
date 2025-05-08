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
		//private Mock<ILogger<JogoController>> _logger;

		//private UsuariosController _controller;
		private Mock<IUsuarioService> _serviceMock;

		public UsuarioControllerTest()
		{
			_serviceMock = new Mock<IUsuarioService>();
			//_controller = new UsuariosController(HelperTests.GetInMemoryContext(), _serviceMock.Object);								
		}

		[Fact]
		public async Task Get_Usuarios_RetornarTodosUsuarios()
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

		[Fact]
		public async Task GetUsuario_ReturnsUser_WhenFound()
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

		[Fact]
		public async Task PostUsuario_CreatesNewUser()
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

		[Fact]
		public async Task DeleteUsuario_RemovesUser_WhenFound()
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

		[Fact]
		public async Task PutUsuario_UpdatesUser_WhenIdsMatch()
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

		[Fact]
		public async Task GetUsuario_ReturnsNotFound_WhenUserDoesNotExist()
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _serviceMock.Object);
			// Act
			var result = await controller.GetUsuario(999);
			// Assert
			Assert.IsType<NotFoundResult>(result.Result);
		}

		[Fact]
		public async Task PutUsuario_ReturnsBadRequest_WhenIdMismatch()
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
	}
}