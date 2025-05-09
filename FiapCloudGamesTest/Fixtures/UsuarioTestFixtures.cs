using Bogus;
using FiapCloudGamesAPI.Models;

public class UsuarioTestFixtures
{
	private readonly Faker _faker;
	public UsuarioTestFixtures()
	{
		_faker = new Faker();
	}

	public Usuario GerarUsuario()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Name.FirstName;
		var sobrenome = _faker.Name.LastName;
		var apelido = _faker.Internet.UserName;
		var email = _faker.Internet.Email;
		var hashSenha = _faker.Internet.Password(length: 8,
											memorable: false,
											regexPattern: "\"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,12}$\"")
											.GetHashCode();
		var dataNascimento = _faker.Date.Past(yearsToGoBack: 100);
		var dataCriacao = _faker.Date.Between(dataNascimento, DateTime.Now);
		var criadoPor = _faker.Name.FirstName;
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName;
		var idPerfil = _faker.UniqueIndex;

		//RegexPattern => Entre 8 e 10 caracteres pelo menos um caracter maisculo, um minusculo, one caracter especial
		//prefix:""
		var usuario = new Usuario(id, nome, sobrenome, apelido, 
			email, hashSenha, dataNascimento, dataCriacao, 
			criadoPor, dataAtualizacao, atualizadoPor, idPerfil);

		return usuario;
	}

	public Usuario GerarUsuarioComSenhaInvalida()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Name.FirstName;
		var sobrenome = _faker.Name.LastName;
		var apelido = _faker.Internet.UserName;
		var email = _faker.Internet.Email;
		var hashSenha = _faker.Internet.Password(length: 6,
											memorable: false,
											regexPattern: "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{4,6}$")
											.GetHashCode();
		//RegexPattern => Entre 4 e 6 caracteres com pelo menos uma letra e um numero
		var dataNascimento = _faker.Date.Past(yearsToGoBack: 100);
		var dataCriacao = _faker.Date.Between(dataNascimento, DateTime.Now);
		var criadoPor = _faker.Name.FirstName;
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName;
		var idPerfil = _faker.UniqueIndex;

		//RegexPattern => Entre 8 e 10 caracteres pelo menos um caracter maisculo, um minusculo, one caracter especial
		//prefix:""
		var usuario = new Usuario(id, nome, sobrenome, apelido,
			email, hashSenha, dataNascimento, dataCriacao,
			criadoPor, dataAtualizacao, atualizadoPor, idPerfil);

		return usuario;
	}

	public Usuario GerarUsuarioComEmailInvalido()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Name.FirstName;
		var sobrenome = _faker.Name.LastName;
		var apelido = _faker.Internet.UserName;
		var email = _faker.Internet.Url;
		var hashSenha = _faker.Internet.Password(length: 8,
											memorable: false,
											regexPattern: "\"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,12}$\"")
											.GetHashCode();
		var dataNascimento = _faker.Date.Past(yearsToGoBack: 100);
		var dataCriacao = _faker.Date.Between(dataNascimento, DateTime.Now);
		var criadoPor = _faker.Name.FirstName;
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName;
		var idPerfil = _faker.UniqueIndex;

		//RegexPattern => Entre 8 e 10 caracteres pelo menos um caracter maisculo, um minusculo, one caracter especial
		//prefix:""
		var usuario = new Usuario(id, nome, sobrenome, apelido,
			email, hashSenha, dataNascimento, dataCriacao,
			criadoPor, dataAtualizacao, atualizadoPor, idPerfil);

		return usuario;
	}

}