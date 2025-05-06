using Bogus;
using Bogus.Extensions;
public class JogoTestFixtures
{
	private readonly Faker _faker;
	public JogoTestFixtures()
	{
		_faker = new Faker();
	}
	public void GerarJogo()
	{
		//Arrange
		var id = _faker.Random;
		var nome = _faker.Name.FullName();		
		var preco = _faker.Random.UInt(min: 0, max: 500);
		var promocao = _faker.Random.Decimal2(min: 0, max: 100);
	}

	public void GerarJogoSemNome()
	{
		//Arrange
		var id = _faker.Random;
		var nome = string.Empty;
		var preco = _faker.Random.UInt(min: 0, max: 500);
		var promocao = _faker.Random.Decimal2(min: 0, max: 100);
	}
}