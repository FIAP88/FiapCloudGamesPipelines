namespace FiapCloudGamesTest.Data
{
	public class UsuarioTestData
	{
		public static IEnumerable<object[]> ApelidoBadRequestData =>
		new List<object[]>
		{
			new object[] {"W", "Apelido deve conter no mínimo 2 caracteres."},	// Apelido inválido
			new object[] {"", "Apelido deve conter no mínimo 2 caracteres."},	// Apelido vazio
			new object[] {"Leo", "Ja existe um jogador Leo"},					// Apelido já existente
		};

		public static IEnumerable<object[]> NomeBadRequestData =>
		new List<object[]>
		{
			new object[] {"Np"},	// Nome inválido
			new object[] {""},		// Nome vazio
		};

		public static IEnumerable<object[]> SenhaBadRequestData =>
		new List<object[]>
		{
			new object[] {"123abcde"},	// Senha inválida (sem caractere especial)
			new object[] {"senha123"},	// Senha inválida (sem maiúscula, sem especial)
			new object[] {"senhaBCD"},	// Senha inválida (sem numero)
			new object[] {""},			// Senha vazia	
		};

		public static IEnumerable<object[]> EmailBadRequestData =>
		new List<object[]>
		{
			new object[] {"emailinvalido"},	// Email inválido
			new object[] {""},				// Email vazio
		};
	}
}
