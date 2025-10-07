using FiapCloudGamesAPI.EventStore.Domain.Eventos.Base;
using FiapCloudGamesAPI.EventStore.Domain.Eventos.Usuario;

namespace FiapCloudGamesAPI.EventSourcing.Agregados
{
	public class UsuarioAggregate
	{
		#region propriedades
		public string? Id { get; private set; }
		public string? Nome { get; private set; }
		public string? Sobrenome { get; private set; }		
		public string? Email { get; set; }
		public DateTime DataNascimento { get; private set; }
		public int Version { get; private set; }

		private readonly List<DomainEvent> _uncommittedEvents = new();
		#endregion

		#region construtor
		// Construtor para criar um novo agregado
		public UsuarioAggregate(string aggregateId, 
			string nome, 
			string sobreNome, 
			string apelido,
			string email,
			string hashSenha,
			DateTime dataNascimento,
			long perfilId)
		{
			var @event = new UsuarioCriado
			{
				AggregateId = aggregateId,
				Nome = nome,
				Sobrenome = sobreNome,
				Apelido = apelido,
				Email = email,
				HashSenha = hashSenha,
				DataNascimento = dataNascimento,
				PerfilId = perfilId,
				Version = 1
			};

			Apply(@event);
			_uncommittedEvents.Add(@event);
		}

		// Construtor privado para (Replay, When, History)
		private UsuarioAggregate() 
		{ 
		}
		#endregion

		#region Logica de negócio
		public void AlterarNome(string novoNome)
		{
			if (novoNome == Nome)
				return;

			var @event = new UsuarioNomeAlterado
			{
				AggregateId = Id,
				NovoNome = novoNome,
				Version = Version + 1
			}; 

			Apply(@event);
			_uncommittedEvents.Add(@event);
		}
		public void AlterarEmail(string novoEmail)
		{
			if (Email == novoEmail) return;

			var @event = new UsuarioEmailAlterado
			{
				AggregateId = Id,
				NovoEmail = novoEmail,
				Version = Version + 1
			};

			Apply(@event);
			_uncommittedEvents.Add(@event);
		}
		public void AlterarSobrenome(string novoSobrenome)
		{
			if (Sobrenome == novoSobrenome) return;

			var @event = new UsuarioSobrenomeAlterado
			{
				AggregateId = Id,
				NovoSobrenome = novoSobrenome,
				Version = Version + 1
			};

			Apply(@event);
			_uncommittedEvents.Add(@event);
		}
		public void AlterarSenha(string novoHashSenha)
		{			
			var @event = new UsuarioSenhaAlterada
			{
				AggregateId = Id,
				NovoHashSenha = novoHashSenha,
				Version = Version + 1
			};

			Apply(@event);
			_uncommittedEvents.Add(@event);
		}
		public void AlterarDadosUsuario(string apelido, DateTime dataNascimento, long perfilId)
		{
			var @event = new UsuarioDadosAlterados
			{
				AggregateId = Id,
				Apelido = apelido,
				DataNascimento = dataNascimento,
				PerfilId = perfilId
			};
			
			Apply(@event);
			_uncommittedEvents.Add(@event);
		}
		// TODO .... 1 metodo para cada evento
		#endregion

		private void Apply(object @event)
		{
			switch (@event)
			{
				case UsuarioCriado criado:
					Id = criado.AggregateId;
					Nome = criado.Nome;
					Sobrenome = criado.Sobrenome;
					Email = criado.Email;
					DataNascimento = criado.DataNascimento;
					Version = criado.Version;
					break;
				case UsuarioDadosAlterados dadosAlterados:
					Version = dadosAlterados.Version;
					break;	
				case UsuarioNomeAlterado nomeAlterado:
					Nome = nomeAlterado.NovoNome;
					Version = nomeAlterado.Version;
					break;
				case UsuarioSobrenomeAlterado sobrenomeAlterado:
					Sobrenome = sobrenomeAlterado.NovoSobrenome;
					Version = sobrenomeAlterado.Version;
					break;
				case UsuarioEmailAlterado emailAlterado:
					Email = emailAlterado.NovoEmail;
					Version = emailAlterado.Version;
					break;
				case UsuarioSenhaAlterada senhaAlterada:					
					Version = senhaAlterada.Version;
					break;
				case UsuarioDesativado desativado:
					Version	= desativado.Version;
					break;
				case JogoAdicionadoAoUsuario jogoAdicionado:
					Version = jogoAdicionado.Version;
					break;
				case AvaliaçãoAdicionadaPeloUsuario avaliacaoAdicionada:
					Version = avaliacaoAdicionada.Version;
					break;
			}			
		}

		public IEnumerable<DomainEvent> GetUncommittedEvents()
		{
			return _uncommittedEvents.AsReadOnly();
		}

		public void MarkEventsAsCommitted()
		{
			_uncommittedEvents.Clear();
		}

		public static UsuarioAggregate FromHistory(IEnumerable<DomainEvent> history)
		{
			var usuario = new UsuarioAggregate();
			foreach (var evt in history.OrderBy(e => e.Version))
			{
				usuario.Apply(evt);
			}
			usuario.MarkEventsAsCommitted();
			return usuario;
		}

	}
}
