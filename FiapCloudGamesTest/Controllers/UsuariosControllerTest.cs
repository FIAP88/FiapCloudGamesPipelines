using FiapCloudGamesAPI.Controllers;
using FiapCloudGamesAPI.Entidades.Dtos;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesTest.Data;
using FiapCloudGamesTest.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
			_httpContextMock = new Mock<IHttpContextAccessor>();
            _baseLoggerMock = new Mock<BaseLogger<Usuario>>(_loggerMock.Object, _correlationIdMock.Object, HelperTests.GetInMemoryContext(), _httpContextMock.Object);
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

            int numeroAtualDeUsuarios = context.Usuarios.Count();

            context.AddRange(usuarioFactory.Generate(2).ToList());
			await context.SaveChangesAsync();

			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);		

			//Act
			var result = await controller.GetUsuarios();

			//Assert
			var actionResult = Assert.IsType<ActionResult<IEnumerable<UsuarioDto>>>(result);
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var users = Assert.IsAssignableFrom<IEnumerable<UsuarioDto>>(okResult.Value);
			Assert.Equal(numeroAtualDeUsuarios + 2, users.Count());
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
		public async Task Get_RetornaNotFound_QuandoInexistente()
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

		[Theory(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação do apelido")]
		[Trait("Usuarios", "Validando Controller")]
		[MemberData(nameof(UsuarioTestData.ApelidoBadRequestData), MemberType = typeof(UsuarioTestData))]
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

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains(mensagemErro, erros);
		}

		[Theory(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação do nome")]
		[Trait("Usuarios", "Validando Controller")]
		[MemberData(nameof(UsuarioTestData.NomeBadRequestData), MemberType = typeof(UsuarioTestData))]
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

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Nome deve conter no mínimo 3 caracteres.", erros);
		}

		[Theory(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação da senha")]
		[Trait("Usuarios", "Validando Controller")]
		[MemberData(nameof(UsuarioTestData.SenhaBadRequestData), MemberType = typeof(UsuarioTestData))]
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

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Senha deve ter no mínimo 8 caracteres, com letras, números e um caractere especial.", erros);	
		}

		[Theory(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação do email")]
		[Trait("Usuarios", "Validando Controller")]
		[MemberData(nameof(UsuarioTestData.EmailBadRequestData), MemberType = typeof(UsuarioTestData))]
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

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Email inválido.", erros);
		}

		[Fact(DisplayName = "PostUsuario deve retornar BadRequest quando falhar na validação da data")]
		[Trait("Usuarios", "Validando Controller")]		
		public async Task Post_RetornaBadRequest_QuandoDataInvalida()
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestFaker().Generate();
			usuarioRequest.DataNascimento = default;

			// Act
			var result = await controller.PostUsuario(usuarioRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Data de nascimento é obrigatória.", erros);
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
		public async Task Put_RetornaBadRequest_QuandoIdDiferente()
		{
            // Arrange
            var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
            var context = HelperTests.GetInMemoryContext();
			
			context.Add(usuario);
            await context.SaveChangesAsync();
            context.Entry(usuario).State = EntityState.Detached;
            
			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario);
			var IdExistente = usuarioRequest.Id;
			usuarioRequest.Id = usuarioRequest.Id + 10;

            var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			// Act
			var result = await controller.PutUsuario(IdExistente, usuarioRequest);
			// Assert
			Assert.IsType<BadRequestResult>(result);
		}

        [Fact(DisplayName = "PutUsuario deve retornar BadRequestObject quando o ID informado não existe")]
        [Trait("Usuarios", "Validando Controller")]
        public async Task Put_RetornaBadRequestObject_QuandoIdNaoExiste()
        {
            // Arrange
            var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestFaker().Generate();
            var context = HelperTests.GetInMemoryContext();
            var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
            // Act
            var result = await controller.PutUsuario(usuarioRequest.Id, usuarioRequest);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory(DisplayName = "PutUsuario deve retornar BadRequest quando falhar na validação do apelido")]
		[Trait("Usuarios", "Validando Controller")]
		[MemberData(nameof(UsuarioTestData.ApelidoBadRequestData), MemberType = typeof(UsuarioTestData))]
		public async Task Put_RetornaBadRequest_QuandoApelidoInvalido(string apelidoInvalido, string mensagemErro)
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			var usuario2 = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
            usuario.Apelido = "Leo";
			usuario2.Apelido = "Leo2";
            context.AddRange(usuario, usuario2);
			await context.SaveChangesAsync();
			context.Entry(usuario).State = EntityState.Added;
            context.Entry(usuario2).State = EntityState.Added;

            usuario2.Apelido = apelidoInvalido;
			var usuario2Request = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario2);

			// Act
			var result = await controller.PutUsuario(usuario2.Id,usuario2Request);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains(mensagemErro, erros);
		}

		[Theory(DisplayName = "PutUsuario deve retornar BadRequest quando falhar na validação do nome")]
		[Trait("Usuarios", "Validando Controller")]
		[MemberData(nameof(UsuarioTestData.NomeBadRequestData), MemberType = typeof(UsuarioTestData))]
		public async Task Put_RetornaBadRequest_QuandoNomeInvalido(string nomeInvalido)
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();			
			context.Add(
				usuario
			);
			await context.SaveChangesAsync();
			context.Entry(usuario).State = EntityState.Detached;

			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario);
			usuarioRequest.Nome = nomeInvalido;

			// Act
			var result = await controller.PutUsuario(usuario.Id, usuarioRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Nome deve conter no mínimo 3 caracteres.", erros);
		}

		[Theory(DisplayName = "PutUsuario deve retornar BadRequest quando falhar na validação da senha")]
		[Trait("Usuarios", "Validando Controller")]
		[MemberData(nameof(UsuarioTestData.SenhaBadRequestData), MemberType = typeof(UsuarioTestData))]
		public async Task Put_RetornaBadRequest_QuandoSenhaInvalida(string senhaInvalida)
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			context.Add(
				usuario
			);
			await context.SaveChangesAsync();
			context.Entry(usuario).State = EntityState.Detached;

			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario);
			usuarioRequest.Senha = senhaInvalida;

			// Act
			var result = await controller.PutUsuario(usuario.Id, usuarioRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Senha deve ter no mínimo 8 caracteres, com letras, números e um caractere especial.", erros);
		}

		[Theory(DisplayName = "PutUsuario deve retornar BadRequest quando falhar na validação do email")]
		[Trait("Usuarios", "Validando Controller")]
		[MemberData(nameof(UsuarioTestData.EmailBadRequestData), MemberType = typeof(UsuarioTestData))]
		public async Task Put_RetornaBadRequest_QuandoEmailInvalido(string emailInvalido)
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			context.Add(
				usuario
			);
			await context.SaveChangesAsync();
			context.Entry(usuario).State = EntityState.Detached;

			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario);
			usuarioRequest.Email = emailInvalido;

			// Act
			var result = await controller.PutUsuario(usuario.Id, usuarioRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Email inválido.", erros);
		}

		[Fact(DisplayName = "PutUsuario deve retornar BadRequest quando falhar na validação da data")]
		[Trait("Usuarios", "Validando Controller")]
		public async Task Put_RetornaBadRequest_QuandoDataInvalida()
		{
			// Arrange
			var context = HelperTests.GetInMemoryContext();
			var controller = new UsuariosController(context, _baseLoggerMock.Object, _httpContextMock.Object);
			var usuario = UsuarioTestFixtures.GerarUsuarioFaker().Generate();
			context.Add(
				usuario
			);
			await context.SaveChangesAsync();
			context.Entry(usuario).State = EntityState.Detached;

			var usuarioRequest = UsuarioTestFixtures.GerarUsuarioRequestByUsuario(usuario);
			usuarioRequest.DataNascimento = default;

			// Act
			var result = await controller.PutUsuario(usuario.Id, usuarioRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

			dynamic value = badRequestResult.Value!;
			var errosProp = value.GetType().GetProperty("Erros");
			var erros = errosProp.GetValue(value) as List<string>;

			Assert.NotNull(erros);
			Assert.Contains("Data de nascimento é obrigatória.", erros);
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