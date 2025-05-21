using FiapCloudGamesAPI.Controllers;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesTest.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
//https://learn.microsoft.com/pt-br/aspnet/core/mvc/controllers/testing?view=aspnetcore-9.0
//https://www.macoratti.net/15/09/mvc_mdlst1.htm

namespace FiapCloudGamesTest.Controllers
{
	public class UsuariosControllerTest
	{
		#region Depedências
		private Mock<ILogger<Usuario>> _loggerMock;
		private Mock<ICorrelationIdGenerator> _correlationIdMock;
		private Mock<BaseLogger<Usuario>> _baseLoggerMock;
		private Mock<IHttpContextAccessor> _httpContextMock; 		

		//private Mock<IUsuarioService> _serviceMock;
		#endregion

		#region Construtor
		public UsuarioControllerTest()
		{
			_loggerMock = new Mock<ILogger<Usuario>>();
			_correlationIdMock = new Mock<ICorrelationIdGenerator>();
			_baseLoggerMock = new Mock<BaseLogger<Usuario>>(_loggerMock.Object, _correlationIdMock.Object);
			_httpContextMock = new Mock<IHttpContextAccessor>();			
			//_controller = new UsuariosController(HelperTests.GetInMemoryContext(), _serviceMock.Object);
		}
		#endregion

		#region Requests
		[Fact(DisplayName = "GetUsuarios deve retornar todos os usuários")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Get_RetornaTodosUsuarios()
		{
			//Arrange
			var usuarioFactory = UsuarioTestFixtures.GerarUsuarioFaker();
			var context = HelperTests.GetInMemoryContext();
			context.AddRange(
				usuarioFactory.Generate(2).ToList()
			);

			await context.SaveChangesAsync();

			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);		

			//Act
			var result = await controller.GetUsuarios();

			//Assert
			var actionResult = Assert.IsType<ActionResult<IEnumerable<Usuario>>>(result);
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var users = Assert.IsAssignableFrom<IEnumerable<Usuario>>(okResult.Value);
			Assert.Equal(2, users.Count());
		}

		[Fact(DisplayName = "GetUsuario deve retornar um usuário existente")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Get_RetornaUsuario()
		{
			//Arrange
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			var context = HelperTests.GetInMemoryContext();
			context.Add(
				usuario
			//new Usuario { UserId = 1, Name = "Leo" }				
			);
			await context.SaveChangesAsync();

			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);

			//Act
			var result = await controller.GetUsuario(usuario.Id);

			//Assert
			var actionResult = Assert.IsType<ActionResult<Usuario>>(result);
			var foundUser = Assert.IsType<Usuario>(actionResult.Value);
			Assert.Equal(usuario.Nome, foundUser.Nome);

			//Assert.Equal("Leo", foundUser.Nome);
		}

		[Fact(DisplayName = "PostUsuario deve criar um novo usuário")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Post_CriaNovoUsuario()
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			//var novoUsuario = new Usuario { UserId = 3, Name = "Fernanda" };
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestValido(usuario);

			// Act
			var result = await controller.PostUsuario(usuarioRequest);

			// Assert
			var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
			var createdUser = Assert.IsType<Usuario>(createdAt.Value);
			Assert.Equal(usuario.Nome, createdUser.Nome);
		}

		[Fact(DisplayName = "DeleteUsuario deve remover usuário existente")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Delete_RemoveUsuarioQuandoEncontrado()
		{
			//Arrange
			var context = HelperTests.GetInMemoryContext();
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			context.Add(
				usuario
				//new Usuario { UserId = 4, Name = "Pedro" }
			);
			await context.SaveChangesAsync();

			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);

			//Act
			//var result = await controller.DeleteUsuario(4);
			var result = await controller.DeleteUsuario(Convert.ToInt64(usuario.Id));

			//Assert
			Assert.IsType<NoContentResult>(result);
			Assert.False(context.Usuarios.Any(u => u.Id == usuario.Id));
		}

		[Fact(DisplayName = "PutUsuario deve atualizar usuário quando IDs forem iguais")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Put_AtualizaUsuarioComMesmoID()
		{
			//Arrange
			//var usuario = new Usuario { UserId = 5, Name = "Ana" };
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			var context = HelperTests.GetInMemoryContext();
			context.Add(
				usuario
			);
			await context.SaveChangesAsync();

			usuario.Nome = "Nome Atualizado";
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestValido(usuario);

			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);

			// Act
				var result = await controller.PutUsuario(usuario.Id, usuarioRequest);

			// Assert
			Assert.IsType<BadRequestResult>(result);
			Assert.Equal("Nome Atualizado", context.Usuarios.Find(usuario.Id)?.Nome);
		}

		[Fact(DisplayName = "GetUsuario deve retornar NotFound para usuário inexistente")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task GetUsuario_RetornaNotFound()
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
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
			// var usuario = new Usuario { UserId = 10, Name = "Carlos" };
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestFaker().Generate();
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			// Act
				var result = await controller.PutUsuario(usuarioRequest.Id + 10, usuarioRequest);
			// Assert
			Assert.IsType<BadRequestResult>(result);
		}
		#endregion

		#region TODO ViewsTests

		#endregion
	}
}