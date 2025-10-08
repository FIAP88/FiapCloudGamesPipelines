using Bogus.Extensions;
using FiapCloudGamesAPI.Models;

public class JogoTestFixtures
{
	#region Dependências
	private readonly Faker _faker;
	#endregion

	#region Construtor
	public JogoTestFixtures()
	{
		_faker = new Faker();
	}
	#endregion

	#region Faker Model
	public Jogo GerarJogo()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Internet.DomainName();
		var descricao = _faker.Lorem.Paragraph();
		var tamanho = _faker.Random.Decimal2(min: 0, max: 1000);
		var preco = _faker.Random.Int(min: 0, max: 500);
		var idCategoria = _faker.UniqueIndex;
		var idadeMinima = _faker.Random.Int(min: 0, max: 500);
		var ativo = _faker.Random.Bool();
		var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
		var criadoPor = _faker.Name.FirstName();
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName();
		var idFornecedor = _faker.UniqueIndex;

		var jogo = new Jogo( nome, descricao, tamanho,
			preco, idCategoria, idadeMinima, ativo, idFornecedor, criadoPor)
		{
			Nome = nome,
			Id = id,
			DataCriacao = dataCriacao,
			DataAtualizacao = dataAtualizacao,
			AtualizadoPor = atualizadoPor,
		};

		return jogo;
	}

	public static Faker<Jogo> GerarJogoFaker()
	{

		var jogoFaker = new Faker<Jogo>("pt_BR")
			.CustomInstantiator(f => new Jogo(
				nome: f.Internet.DomainName(),
				descricao: f.Lorem.Paragraph(),
				tamanho: f.Random.Decimal2(min: 0, max: 1000),
				preco: f.Random.Int(min: 0, max: 500),
				idCategoria: f.UniqueIndex,
				idadeMinima: f.Random.Int(min: 0, max: 500),
				ativo: f.Random.Bool(),
				idFornecedor: f.UniqueIndex,
				criadoPor: f.Name.FirstName()
				) { Nome = f.Internet.DomainName() })			
			.RuleFor(e => e.Id, f => f.UniqueIndex)
			.RuleFor(e => e.DataCriacao, f => f.Date.Past(yearsToGoBack: 100))
			.RuleFor(e => e.DataAtualizacao, (f, e) => f.Date.Between(e.DataCriacao, DateTime.Now))
			.RuleFor(e => e.AtualizadoPor, f => f.Name.FirstName());

		return jogoFaker;
	}

	public Jogo GerarJogoSemNome()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Internet.DomainName();
		var descricao = _faker.Lorem.Paragraph();
		var tamanho = _faker.Random.Decimal2(min: 0, max: 1000);
		var preco = _faker.Random.Int(min: 0, max: 500);
		var idCategoria = _faker.UniqueIndex;
		var idadeMinima = _faker.Random.Int(min: 0, max: 500);
		var ativo = _faker.Random.Bool();
		var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
		var criadoPor = _faker.Name.FirstName();
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName();
		var idFornecedor = _faker.UniqueIndex;

		var jogo = new Jogo(nome, descricao, tamanho,
			preco, idCategoria, idadeMinima, ativo, idFornecedor, criadoPor)
		{
			Nome = nome,
			Id = id,
			DataCriacao = dataCriacao,
			DataAtualizacao = dataAtualizacao,
			AtualizadoPor = atualizadoPor,
		};

		return jogo;
	}
	#endregion

	#region TODO Faker DTOs
	#endregion

	#region TODO Faker Requests
	#endregion
}