using Bogus;
using Bogus.Extensions;
using FiapCloudGamesAPI.Models;

public class JogoTestFixtures
{
	private readonly Faker _faker;
	public JogoTestFixtures()
	{
		_faker = new Faker();
	}
	public Jogo GerarJogo()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = _faker.Internet.DomainName();
		var descricao = _faker.Lorem.Paragraph();
		var tamanho = _faker.Random.Decimal2(min: 0, max: 1000);
		var preco = _faker.Random.UInt(min: 0, max: 500);
		var idCategoria = _faker.UniqueIndex;
		var idadeMinima = _faker.Random.UInt(min: 0, max: 500);
		var ativo = _faker.Random.Bool();
		var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
		var criadoPor = _faker.Name.FirstName;
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName;
		var idFornecedor = _faker.UniqueIndex;

		var jogo = new Jogo(id, nome, descricao, tamanho,
			preco, idCategoria, idadeMinima, ativo, dataCriacao,
			criadoPor, dataAtualizacao, atualizadoPor, idFornecedor);

		return jogo;
	}

	public Jogo GerarJogoSemNome()
	{
		//Arrange
		var id = _faker.UniqueIndex;
		var nome = string.Empty;
		var descricao = _faker.Lorem.Paragraph();
		var tamanho = _faker.Random.Decimal2(min: 0, max: 1000);
		var preco = _faker.Random.UInt(min: 0, max: 500);
		var idCategoria = _faker.UniqueIndex;
		var idadeMinima = _faker.Random.UInt(min: 0, max: 500);
		var ativo = _faker.Random.Bool();
		var dataCriacao = _faker.Date.Past(yearsToGoBack: 100);
		var criadoPor = _faker.Name.FirstName;
		var dataAtualizacao = _faker.Date.Between(dataCriacao, DateTime.Now);
		var atualizadoPor = _faker.Name.FirstName;
		var idFornecedor = _faker.UniqueIndex;

		var jogo = new Jogo(id, nome, descricao, tamanho,
			preco, idCategoria, idadeMinima, ativo, dataCriacao,
			criadoPor, dataAtualizacao, atualizadoPor, idFornecedor);

		return jogo;
	}
}