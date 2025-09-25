namespace FiapCloudGamesAPI.EventSourcing.Eventos.Usuario;

public record UsuarioCriado(Guid UsuarioId) : BaseEvent(UsuarioId);