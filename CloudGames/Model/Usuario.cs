namespace CloudGames.Model
{
    public class Usuario : EntityBase
    {
      
        public required string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string HashSenha { get; set; }
        public int? DataNascimento { get; set; }
        public int PerfilId { get; set; }
    }
}
