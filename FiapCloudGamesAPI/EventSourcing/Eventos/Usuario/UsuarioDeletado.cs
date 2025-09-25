namespace FiapCloudGamesAPI.EventSourcing.Eventos.Usuario;

public record UsuarioDeletado(Guid UsuarioId) : BaseEvent(UsuarioId);