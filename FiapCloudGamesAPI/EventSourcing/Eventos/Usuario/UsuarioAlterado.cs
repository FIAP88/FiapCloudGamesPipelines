namespace FiapCloudGamesAPI.EventSourcing.Eventos.Usuario;

public record UsuarioAlterado(Guid UsuarioId) : BaseEvent(UsuarioId);