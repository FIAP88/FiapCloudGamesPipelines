using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace TechChallenge.Test
{
    public class TestesUnitariosRepositorio
    {
		#region Usuario
		[Fact]
		public async void UsuarioRepositorio_Add_RetornaTrue()
		{		
			//Arrange
			var id = 1;
			var nome = "Leonardo";
			var email = "leo@hotmail.com";
			var senha = "1234@abcd";

			Usuario usuario = new Usuario(id, nome, email, senha);

			var helper = new TestesHelper();
			var usuarioRepository = helper.GetInMemoryRepository<Usuario>();
			
			//Act
			var result = usuarioRepository.Add(usuario);

			//Assert
			Assert.Equal(result, true);
		}

		[Fact]
		public async void UsuarioRepositorio_Add_RetornaFalse()
		{
			//FALSE
			//Devido a propriedade Email ser unica entre as entidades Usuario

			//Arrange
			var primeiroId = 1;
			var primeiroNome = "Leonardo";
			var primeiroEmail = "leo@hotmail.com";
			var primeiroSenha = "1234@abcd";

			var segundoId = 2;
			var segundoNome = "Renato";
			var segundoEmail = "renato@hotmail.com";
			var segundoSenha = "1234@abcd";

			Usuario primeiroUsuario = new Usuario(primeiroId, primeiroNome, primeiroEmail, primeiroSenha);

			Usuario segundoUsuario = new Usuario(segundoId, segundoNome, segundoEmail, segundoSenha);

			var helper = new TestesHelper();
			var usuarioRepository = helper.GetInMemoryRepository<Usuario>();

			//Act
			usuarioRepository.Add(primeiroUsuario);
			var result = usuarioRepository.Add(segundoUsuario);

			//Assert
			Assert.Equal(result, false);
		}

		
		public async void Usuario_List()
		{
			using (var dbContext = await GetDbContext())
			{
				foreach (var usuario in dbContext.Usuarios)
				{
					Console.WriteLine(JsonSerializer.Serialize(usuario));
				}
			}
		}
		#endregion

		#region Jogo
		[Fact]
		public async void JogoRepositorio_Add_RetornaTrue()
		{
			//Arrange
			var id = 1;
			var nome = "Heroes of Mighty and Magic 3";
			var preco = 99.99;  
			var promocao = 0.10;

			Jogo jogo = new Jogo(id, nome, preco, promocao);

			var helper = new TestesHelper();
			var jogoRepository = helper.GetInMemoryRepository<Jogo>();

			//Act
			var result = jogoRepository.Add(jogo);

			//Assert
			Assert.Equal(result, true);
		}

		[Fact]
		public async void JogoRepositorio_Add_RetornaFalse()
		{
			//FALSE
			//Devido a propriedade Nome dever ser unica entre as entidades Jogo

			//Arrange
			var primeiroId = 1;
			var segundoId = 2;
			var nome = "Dota";
			var preco = 99.99;
			var promocao = 0.10;			

			Jogo primeiroJogo = new Jogo(primeiroId, nome, preco, promocao);
			Jogo segundoJogo = new Jogo(segundoId, nome, preco, promocao);

			var helper = new TestesHelper();
			var jogoRepository = helper.GetInMemoryRepository<Jogo>();

			//Act
			jogoRepository.Add(primeiroJogo);
			var result = jogoRepository.Add(segundoJogo);

			//Assert			
			Assert.Equal(result, false);
		}
		
		public async void Jogo_List()
		{
			var helper = new TestesHelper();
			var jogoRepository = helper.GetInMemoryRepository<Jogo>();

			jogoRepository.ListAll();
			using (var dbContext = await GetDbContext())
			{
				foreach (var jogo in dbContext.Jogos)
				{
					Console.WriteLine(JsonSerializer.Serialize(jogo));
				}
			}
		}
		#endregion
	}
}
