namespace TechChallenge.Test
{
    public class TestesUnit�riosDom�nio
    {
        #region Usu�rio
        [Fact]
        public void CriarUsuario_Valido()
        {
            //Arrange
            var id = 1;
            var nome = "Leonardo";
            var email = "leo@hotmail.com";
            var senha = "1234@abcd";

            //Act
            Usuario usuario = new Usuario(id, nome, email, senha );
            //Debug.WriteLine(JsonSerializer.Serialize(usuario));
            
            //Assert
            Assert.Equal(id, usuario.Id);
            Assert.Equal(nome, usuario.Nome);
            Assert.Equal(email, usuario.Email);
            Assert.Equal(senha, usuario.Senha);
        }

        [Fact]
        public void CriarUsuario_SenhaInvalida()
		{
            //Arrange
            var id = 1;
            var nome = "Leonardo";
            var email = "leo@hotmail.com";
            var senha = "1234abcd";

            //Act
            var ex = Assert.Throws<ArgumentException>(() =>
                new Usuario(id, nome, email, senha));

            //Assert
            Assert.Equal("Senha inv�lida", ex.Message);
        }


        [Fact]
        public void CriarUsuario_EmailInvalido()
		{
            //Arrange
            var id = 1;
            var nome = "Leonardo";
            var email = "leotmail.com";
            var senha = "1234@abcd";

            //Act
            var ex = Assert.Throws<ArgumentException>(() =>
                        new Usuario(id, nome, email, senha));

            //Assert
            Assert.Equal("Email inv�lido", ex.Message);
        }
        #endregion             
                                                 
        #region Jogos           
        [Fact]
        public void CriarJogo_Valido()
		{
            //Arrange
            var id = 1;
            var nome = "Heroes of Mighty and Magic 3";
            var preco = 99.99;// reais?      
            var promocao = 0.10; // 10% de desconto 
            //Data(validade) da promo��o
            //Data de cria��o
            //categoria
                         
            //Act
            Jogo jogo = new Jogo( id, nome, preco, promocao );

            //Assert
            Assert.Equal(id, jogo.Id);
            Assert.Equal(nome, jogo.Nome);
            Assert.Equal(preco, jogo.Preco);
            Assert.Equal(promocao, jogo.Promocao);
        }

        [Fact]
        public void CriarJogo_NomeInvalido()
		{
            //Arrange
            var id = 1;
            //string nome = null;
            var preco = 199.99;
            var promocao = 0.10;
            
            //Act
            var ex = Assert.Throws<ArgumentException>(() =>
                        new Jogo(id, null, preco, promocao));

            //Assert
            Assert.Equal("Nome inv�lido", ex.Message);
        }	
		#endregion
	}
}        