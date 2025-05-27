using Bogus.DataSets;
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

		#region GET
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
		#endregion

		#region POST

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

		// MENSAGENS DE ERRO POSSÍVEIS
		// Nome deve conter no mínimo 3 caracteres.
		// Apelido deve conter no mínimo 2 caracteres.
		// Senha deve ter no mínimo 8 caracteres, com letras, números e um caractere especial.
		// Email inválido.
		// Data de nascimento é obrigatória.
		// "Ja existe um jogador " + usuario.Apelido

		[Theory(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação do apelido")]
		[Trait("Usuarios", "Validando Controller")]
		[InlineData("W", "Apelido deve conter no mínimo 2 caracteres.")]	// Apelido inválido
		[InlineData("", "Apelido deve conter no mínimo 2 caracteres.")]		// Apelido vazio
		[InlineData("Leo", "Ja existe um jogador Leo")]						// Apelido já existente
		public async Task Post_RetornaBadRequest_QuandoApelidoInvalido(string apelidoInvalido, string mensagemErro)
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			usuario.Apelido = "Leo";
			context.Add(
				usuario
			);
			await context.SaveChangesAsync();
			context.Entry(usuario).State = EntityState.Detached;

			usuario.Apelido = apelidoInvalido;
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario);

			// Act
			var result = await controller.PostUsuario(usuarioRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

			dynamic value = badRequestResult.Value;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains(mensagemErro, erros);
		}

		[Theory(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação do nome")]
		[Trait("Usuarios", "Validando Controller")]
		[InlineData("Np")]   // Nome inválido
		[InlineData("")]     // Nome vazio
		public async Task Post_RetornaBadRequest_QuandoNomeInvalido(string nomeInvalido)
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestFaker().Generate();
			usuarioRequest.Nome = nomeInvalido;

			// Act
			var result = await controller.PostUsuario(usuarioRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

			dynamic value = badRequestResult.Value;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Nome deve conter no mínimo 3 caracteres.", erros);
		}

		[Theory(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação da senha")]
		[Trait("Usuarios", "Validando Controller")]
		[InlineData("123abcde")]      // Senha inválida (sem caractere especial)
		[InlineData("senha123")]      // Senha inválida (sem maiúscula, sem especial)
		[InlineData("senhaBCD")]      // Senha inválida (sem numero)
		[InlineData("")]              // Senha vazia		
		public async Task Post_RetornaBadRequest_QuandoSenhaInvalida(string senhaInvalida)
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);			
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestFaker().Generate();
			usuarioRequest.Senha = senhaInvalida;

			// Act
			var result = await controller.PostUsuario(usuarioRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

			dynamic value = badRequestResult.Value;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Senha deve ter no mínimo 8 caracteres, com letras, números e um caractere especial.", erros);	
		}

		[Theory(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação do email")]
		[Trait("Usuarios", "Validando Controller")]
		[InlineData("emailinvalido")]   // Email inválido
		[InlineData("")]                // Email vazio
		public async Task Post_RetornaBadRequest_QuandoEmailInvalido(string emailInvalido)
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestFaker().Generate();
			usuarioRequest.Email = emailInvalido;

			// Act
			var result = await controller.PostUsuario(usuarioRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

			dynamic value = badRequestResult.Value;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Email inválido.", erros);
		}

		#endregion

		#region PUT
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

		[Fact(DisplayName = "PutUsuario deve retornar BadRequest quando IDs forem diferentes")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task PutUsuario_RetornaBadRequest_QuandoIdDiferente()
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

		#region DELETE
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
		#endregion

		#endregion

		#region TODO: ViewsTests

		#endregion
	}
}