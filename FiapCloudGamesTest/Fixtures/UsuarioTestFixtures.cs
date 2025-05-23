using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;
using FiapCloudGamesAPI.Entidades.Requests;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesTest.Fixtures;
using System.Text;

public class UsuarioTestFixtures
{
	private readonly Faker _faker;
	public UsuarioTestFixtures()
	{
		_faker = new Faker();
	}

	public static string GerarSenhaSegura(int minLength = 8, int maxLength = 12)
	{
		const string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
		const string letrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		const string numeros = "0123456789";
		const string especiais = "@$!%*?&";

		var random = new Random();

		// Define o comprimento aleatório
		int comprimento = random.Next(minLength, maxLength + 1);

		// Garante pelo menos um de cada categoria
		var senha = new StringBuilder();
		senha.Append(letrasMinusculas[random.Next(letrasMinusculas.Length)]);
		senha.Append(letrasMaiusculas[random.Next(letrasMaiusculas.Length)]);
		senha.Append(numeros[random.Next(numeros.Length)]);
		senha.Append(especiais[random.Next(especiais.Length)]);

		// Preenche o restante com caracteres aleatórios permitidos
		string todosCaracteres = letrasMinusculas + letrasMaiusculas + numeros + especiais;
		for (int i = senha.Length; i < comprimento; i++)
		{
			senha.Append(todosCaracteres[random.Next(todosCaracteres.Length)]);
		}

		// Embaralha os caracteres para não ficarem previsíveis
		return new string(senha.ToString().OrderBy(c => random.Next()).ToArray());
	}

	public static Faker<Usuario> GerarUsuarioFaker()
	{
		var perfilFactory = PerfilTestFixture.GerarPerfilFaker();
		var avalicaoFactory = AvaliacaoTestFixtures.GerarAvaliacaoFaker();

		var usuarioValidoFaker = new Faker<Usuario>("pt_BR")
			.CustomInstantiator(f => new Usuario(
				nome: f.Internet.DomainName(),
				sobrenome: f.Name.LastName(),
				apelido: f.Internet.UserName(),
				email: f.Internet.Email(),
				hashSenha: GerarSenhaSegura(),
							//f.Internet.Password(length: 8,
							//				memorable: false,
							//				regexPattern: "\"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,12}$\""),
				dataNascimento: f.Date.Past(yearsToGoBack: 100),
				perfilId: f.UniqueIndex,
				criadoPor: f.Name.FirstName()
				)
			{ Nome = f.Name.FirstName()})
			.RuleFor(e => e.Id, f => f.UniqueIndex)
			.RuleFor(e => e.DataCriacao, (f, e) => f.Date.Between(e.DataNascimento, DateTime.Now))
			.RuleFor(e => e.DataAtualizacao, (f, e) => f.Date.Between(e.DataCriacao, DateTime.Now))
			.RuleFor(e => e.AtualizadoPor, f => f.Name.FirstName())
			.RuleFor(e => e.Avaliacoes,  f => avalicaoFactory.Generate(3).ToList())
			.RuleFor(e => e.Perfil, f => perfilFactory.Generate())
			.RuleFor(e => e.PerfilId, (f, e) => e.Perfil.Id);

		return usuarioValidoFaker;
	}

	public static Faker<UsuarioRequest> GerarUsuarioRequestFaker()
	{
		var perfilFactory = PerfilTestFixture.GerarPerfilFaker();
		var avalicaoFactory = AvaliacaoTestFixtures.GerarAvaliacaoFaker();

		var usuarioRequestFaker = new Faker<UsuarioRequest>("pt_BR")
			.CustomInstantiator(f => new UsuarioRequest()
			{
				Id = f.UniqueIndex,
				Nome = f.Internet.DomainName(),
				Sobrenome = f.Name.LastName(),
				Apelido = f.Internet.UserName(),
				Email = f.Internet.Email(),
				Senha = GerarSenhaSegura(),
							//f.Internet.Password(length: 8,
							//				memorable: false,
							//				regexPattern: "\"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,12}$\""),
				DataNascimento = f.Date.Past(yearsToGoBack: 100),
				PerfilId = f.UniqueIndex
			});

		return usuarioRequestFaker;
	}

	public static UsuarioRequest GerarUsuarioRequestValido(Usuario usuario)
	{
		var usuarioRequest = new Faker<UsuarioRequest>("pt_BR")
			.CustomInstantiator(f => new UsuarioRequest()
			{
				Id = usuario.Id,
				Nome = usuario.Nome,
				Sobrenome = usuario.Sobrenome,
				Apelido = usuario.Apelido,
				Email = usuario.Email,
				Senha = usuario.HashSenha,
				DataNascimento = usuario.DataNascimento,
				PerfilId = usuario.PerfilId
			});

		return usuarioRequest.Generate();
	}

	public Usuario GerarUsuarioValido()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Name.FirstName();
		var sobrenome = _faker.Name.LastName();
		var apelido = _faker.Internet.UserName();
		var email = _faker.Internet.Email();
		var hashSenha = GerarSenhaSegura(); 
						//_faker.Internet.Password(length: 8,
						//					memorable: false,
						//					regexPattern: "\"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,12}$\"");
				//RegexPattern => Entre 8 e 10 caracteres pelo menos um caracter maisculo, um minusculo, one caracter especial
				//prefix:""
		var dataNascimento = _faker.Date.Past(yearsToGoBack: 100);
		var dataCriacao = _faker.Date.Between(dataNascimento, DateTime.Now);
		var criadoPor = _faker.Name.FirstName();
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName();
		var idPerfil = _faker.UniqueIndex;

		var usuario = new Usuario( nome, sobrenome, apelido, 
			email, hashSenha, dataNascimento, 
			 idPerfil, criadoPor)
		{
			Nome = nome,
			Id = id,
			DataCriacao = dataCriacao,
			DataAtualizacao = dataAtualizacao,
			AtualizadoPor = atualizadoPor,
		};

		return usuario;
	}

	public Usuario GerarUsuarioComSenhaInvalida()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Name.FirstName();
		var sobrenome = _faker.Name.LastName();
		var apelido = _faker.Internet.UserName();
		var email = _faker.Internet.Email();
		var hashSenha = GerarSenhaSegura(minLength: 2 ,maxLength: 4);
			//_faker.Internet.Password(length: 6,
			//					memorable: false,
			//					regexPattern: "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{4,6}$");
			//RegexPattern => Entre 4 e 6 caracteres com pelo menos uma letra e um numero
		var dataNascimento = _faker.Date.Past(yearsToGoBack: 100);
		var dataCriacao = _faker.Date.Between(dataNascimento, DateTime.Now);
		var criadoPor = _faker.Name.FirstName();
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName();
		var idPerfil = _faker.UniqueIndex;

		var usuario = new Usuario(nome, sobrenome, apelido,
			email, hashSenha, dataNascimento,
			 idPerfil, criadoPor)
		{
			Nome = nome,
			Id = id,
			DataCriacao = dataCriacao,
			DataAtualizacao = dataAtualizacao,
			AtualizadoPor = atualizadoPor,
		};

		return usuario;
	}

	public Usuario GerarUsuarioComEmailInvalido()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Name.FirstName();
		var sobrenome = _faker.Name.LastName();
		var apelido = _faker.Internet.UserName();
		var email = _faker.Internet.Url();
		var hashSenha = GerarSenhaSegura();
							//_faker.Internet.Password(length: 8,
							//				memorable: false,
							//				regexPattern: "\"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,12}$\"");
		//RegexPattern => Entre 8 e 10 caracteres pelo menos um caracter maisculo, um minusculo, one caracter especial
		//prefix:""
		var dataNascimento = _faker.Date.Past(yearsToGoBack: 100);
		var dataCriacao = _faker.Date.Between(dataNascimento, DateTime.Now);
		var criadoPor = _faker.Name.FirstName();
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName();
		var idPerfil = _faker.UniqueIndex;

		var usuario = new Usuario(nome, sobrenome, apelido,
			email, hashSenha, dataNascimento,
			 idPerfil, criadoPor)
		{
			Nome = nome,
			Id = id,
			DataCriacao = dataCriacao,
			DataAtualizacao = dataAtualizacao,
			AtualizadoPor = atualizadoPor,
		};

		return usuario;
	}

}