using FiapCloudGamesAPI.Controllers;
using FiapCloudGamesAPI.Entidades.Dtos;
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
		#region Deped�ncias
		private Mock<ILogger<Usuario>> _loggerMock;
		private Mock<ICorrelationIdGenerator> _correlationIdMock;
		private Mock<BaseLogger<Usuario>> _baseLoggerMock;
		private Mock<IHttpContextAccessor> _httpContextMock; 		

		//private Mock<IUsuarioService> _serviceMock;
		#endregion

		#region Construtor
		public UsuariosControllerTest()
		{
			_loggerMock = new Mock<ILogger<Usuario>>();
			_correlationIdMock = new Mock<ICorrelationIdGenerator>();
			_baseLoggerMock = new Mock<BaseLogger<Usuario>>(_loggerMock.Object, _correlationIdMock.Object);
			_httpContextMock = new Mock<IHttpContextAccessor>();
			//_controller = new UsuariosController(HelperTests.GetInMemoryContext(), _serviceMock.Object);
			
		}
		#endregion

		#region Requests
		[Fact(DisplayName = "GetUsuarios deve retornar todos os usu�rios")]
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
			var actionResult = Assert.IsType<ActionResult<IEnumerable<UsuarioDto>>>(result);
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var users = Assert.IsAssignableFrom<IEnumerable<UsuarioDto>>(okResult.Value);
			Assert.Equal(2, users.Count());
		}

		[Fact(DisplayName = "GetUsuario deve retornar um usu�rio existente")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Get_RetornaUsuario()
		{
			//Arrange
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			var context = HelperTests.GetInMemoryContext();
			context.Add(
				usuario			
			);
			await context.SaveChangesAsync();

			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);

			//Act
			var result = await controller.GetUsuario(usuario.Id);

			//Assert
			var actionResult = Assert.IsType<ActionResult<Usuario>>(result);
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var foundUser = Assert.IsAssignableFrom<Usuario>(okResult.Value);
			Assert.Equal(foundUser, usuario);
		}

		[Fact(DisplayName = "PostUsuario deve criar um novo usu�rio")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Post_CriaNovoUsuario()
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario);

			// Act
			var result = await controller.PostUsuario(usuarioRequest);

			// Assert
			var createdAt = Assert.IsType<OkObjectResult>(result.Result);
			var createdUser = Assert.IsType<Usuario>(createdAt.Value);
			Assert.Equal(usuario.Nome, createdUser.Nome);
		}

        // TODO
        // Utilizar do [Theory] e [MemberData] para reutilizar o codigo no teste de validação do Email e Senha invalida
        //     [Fact(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação")]
        //     [Trait("Usuarios", "Validando Controller")]
        //     public async Task Post_CriaNovoUsuarioRetornaBadRequest_QuandoSenhaInvalida()
        //     {
        //         // Arrange
        //         var context = HelperTests.GetInMemoryContext();
        //         var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
        //         var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
        //usuario.HashSenha = "123abcde"; // FALTA CARACTER ESPECIAL
        //         var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario);

        //         // Act
        //         var result = await controller.PostUsuario(usuarioRequest);

        //         // Assert
        //         var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        //         Assert.IsType<SerializableError>(badRequestResult.Value);
        //     }

        [Fact(DisplayName = "DeleteUsuario deve remover usu�rio existente")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Delete_RemoveUsuario_QuandoEncontrado()
		{
			//Arrange
			var context = HelperTests.GetInMemoryContext();
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			context.Add(
				usuario
			);
			await context.SaveChangesAsync();

			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);

			//Act
			var result = await controller.DeleteUsuario(Convert.ToInt64(usuario.Id));

			//Assert
			Assert.IsType<NoContentResult>(result);
			Assert.False(context.Usuarios.Any(u => u.Id == usuario.Id));
		}

		[Fact(DisplayName = "PutUsuario deve atualizar usu�rio quando IDs forem iguais")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Put_AtualizaUsuarioComMesmoID()
		{
			//Arrange
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			var context = HelperTests.GetInMemoryContext();
			context.Add(
				usuario
			);
			await context.SaveChangesAsync();

			usuario.Nome = "Nome Atualizado";

            context.Entry(usuario).State = EntityState.Detached;

		    var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario);

			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);

            // Act
            var result = await controller.PutUsuario(usuario.Id, usuarioRequest);

			// Assert
			Assert.IsType<OkResult>(result);
			Assert.Equal("Nome Atualizado", context.Usuarios.Find(usuario.Id)?.Nome);
		}

		[Fact(DisplayName = "GetUsuario deve retornar NotFound para usu�rio inexistente")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task GetUsuario_RetornaNotFound_QuandoInexistente()
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
		public async Task PutUsuario_ReturnaBadRequest_QuandoIdDiferente()
		{
			// Arrange
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