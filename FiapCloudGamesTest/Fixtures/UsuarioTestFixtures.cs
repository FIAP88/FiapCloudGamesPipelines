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
		var nome = _faker.Name.FullName;
		var email = _faker.Internet.Email;
		var senha = _faker.Internet.Password(length:8,
											memorable:false,
											regexPattern: "\"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,12}$\"");
		//RegexPattern => Entre 8 e 10 caracteres pelo menos um caracter maisculo, um minusculo, one caracter especial
											//prefix:""
		var usuario = new Usuario(id,nome,email,senha);
		return usuario;
		
	}

	//Senha com caracteres insuficientes (entre 4 e 6)
	public Usuario GerarUsuarioComSenhaInvalida()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Name.FullName;
		var email = _faker.Internet.Email;
		var senha = _faker.Internet.Password(length: 8,
											memorable: false,
											regexPattern: "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$");
		//RegexPattern => Entre 8 e 12 caracteres com pelo menos uma letra e um numero

		var usuario = new Usuario(id, nome, email, senha);
		return usuario;
	}

	public Usuario GerarUsuarioComEmailInvalido()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Name.FullName;
		var email = _faker.Name.JobArea; //Gera um nome de serviço aleatório
		var senha = _faker.Internet.Password(length: 8,
											memorable: false,
											regexPattern: "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$");
		//RegexPattern => Entre 8 e 12 caracteres com pelo menos uma letra e um numero

		var usuario = new Usuario(id, nome, email, senha);
		return usuario;
	}

}