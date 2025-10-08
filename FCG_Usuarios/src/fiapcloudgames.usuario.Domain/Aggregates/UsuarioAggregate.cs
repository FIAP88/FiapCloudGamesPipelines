using fiapcloudgames.usuario.Domain.Events;
using fiapcloudgames.usuario.Domain.Events.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Domain.Events.Usuario.DisableUsuario;
using fiapcloudgames.usuario.Domain.Events.Usuario.Outros;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioDados;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioEmail;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioNome;
using fiapcloudgames.usuario.Domain.Events.Usuario.UpdateUsuarioSobrenome;

namespace fiapcloudgames.usuario.Domain.Aggregates
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
			string telefone,
			string hashSenha,
			DateTime dataNascimento,
			long perfilId)
		{
			var @event = new CreateUsuario
			{
				AggregateId = aggregateId,
				Nome = nome,
				Sobrenome = sobreNome,
				Apelido = apelido,				
				Email = email,
				Telefone = telefone,
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

			var @event = new UpdateUsuarioNome
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

			var @event = new UpdateUsuarioEmail
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

			var @event = new UpdateUsuarioSobrenome
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
			var @event = new UpdateUsuarioDados
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
				case CreateUsuario criado:
					Id = criado.AggregateId;
					Nome = criado.Nome;
					Sobrenome = criado.Sobrenome;
					Email = criado.Email;					
					DataNascimento = criado.DataNascimento;
					Version = criado.Version;
					break;
				case UpdateUsuarioDados dadosAlterados:
					Version = dadosAlterados.Version;
					break;	
				case UpdateUsuarioNome nomeAlterado:
					Nome = nomeAlterado.NovoNome;
					Version = nomeAlterado.Version;
					break;
				case UpdateUsuarioSobrenome sobrenomeAlterado:
					Sobrenome = sobrenomeAlterado.NovoSobrenome;
					Version = sobrenomeAlterado.Version;
					break;
				case UpdateUsuarioEmail emailAlterado:
					Email = emailAlterado.NovoEmail;
					Version = emailAlterado.Version;
					break;
				case UsuarioSenhaAlterada senhaAlterada:					
					Version = senhaAlterada.Version;
					break;
				case DisableUsuario desativado:
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
