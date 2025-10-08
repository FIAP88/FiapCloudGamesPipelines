using FiapCloudGamesAPI.Entidades.Requests;
using FiapCloudGamesAPI.Models;
using FiapCloudGamesTest.Fixtures;
using System.Text;

public class UsuarioTestFixtures
{
	#region Faker Model
	public static string GerarSenhaSegura(int minLength = 8, int maxLength = 12)
	{
		const string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
		const string letrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		const string numeros = "0123456789";
		const string especiais = "@$!%*?&";

		var random = new Random();

		// Define o comprimento aleat�rio
		int comprimento = random.Next(minLength, maxLength + 1);

		// Garante pelo menos um de cada categoria
		var senha = new StringBuilder();
		senha.Append(letrasMinusculas[random.Next(letrasMinusculas.Length)]);
		senha.Append(letrasMaiusculas[random.Next(letrasMaiusculas.Length)]);
		senha.Append(numeros[random.Next(numeros.Length)]);
		senha.Append(especiais[random.Next(especiais.Length)]);

		// Preenche o restante com caracteres aleat�rios permitidos
		string todosCaracteres = letrasMinusculas + letrasMaiusculas + numeros + especiais;
		for (int i = senha.Length; i < comprimento; i++)
		{
			senha.Append(todosCaracteres[random.Next(todosCaracteres.Length)]);
		}

		// Embaralha os caracteres para n�o ficarem previs�veis
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
	#endregion

	#region Faker Requests
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
				DataNascimento = f.Date.Past(yearsToGoBack: 100),
				PerfilId = f.UniqueIndex
			});

		return usuarioRequestFaker;
	}

	public static UsuarioRequest GerarUsuarioRequestByUsuario(Usuario usuario)
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
	#endregion
}