using FiapCloudGamesAPI.EventSourcing.Eventos;
using FiapCloudGamesAPI.EventSourcing.Eventos.Usuario;

namespace FiapCloudGamesAPI.EventSourcing.Agregados
{
	public class UsuarioAgreggate // Repository?
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public string Type { get; private set; }
		public List<BaseEvent> Events { get; private set; } = [];

		private UsuarioAgreggate()
		{
			
		}

		#region Metodos dos Eventos
		public static UsuarioAgreggate Create(string Name)
		{
			// Validação para criação de Usuário
			if (string.IsNullOrWhiteSpace(Name))
			{
				throw new ArgumentNullException("Nome de usuario é necessário");
			}
			// .......
			//

			var usuarioAgreggate = new UsuarioAgreggate();

			var @event = new UsuarioCriado(Guid.NewGuid());

			usuarioAgreggate.Apply(@event);

			return usuarioAgreggate;
		}

		public static UsuarioAgreggate Delete(string Name)
		{
			// Validação para remoção de Usuário
			if (string.IsNullOrWhiteSpace(Name))
			{
				throw new ArgumentNullException("Nome de usuario é necessário");
			}
			// .......
			//

			var usuarioAgreggate = new UsuarioAgreggate();

			var @event = new UsuarioDeletado(Guid.NewGuid());

			usuarioAgreggate.Apply(@event);

			return usuarioAgreggate;
		}

		public static UsuarioAgreggate Edit(string Name)
		{
			// Validação para edição do Usuário
			if (string.IsNullOrWhiteSpace(Name))
			{
				throw new ArgumentNullException("Nome de usuario é necessário");
			}
			// .......
			//

			var usuarioAgreggate = new UsuarioAgreggate();

			var @event = new UsuarioAlterado(Guid.NewGuid());

			usuarioAgreggate.Apply(@event);

			return usuarioAgreggate;
		}
		#endregion

		#region Apply Replay
		private void Apply(object @event)
		{
			switch (@event)
			{
				case UsuarioCriado e:
					break;
				case UsuarioAlterado e:
					break;
				case UsuarioDeletado e:
					break;
			}
		}

		public static UsuarioAgreggate ReplayEvents(IEnumerable<BaseEvent> events)
		{
			var usuarioAgreggate = new UsuarioAgreggate();
			foreach (var @event in events)
			{
				usuarioAgreggate.Apply(@event);
			}
			return usuarioAgreggate;
		}
		#endregion
	}
}
