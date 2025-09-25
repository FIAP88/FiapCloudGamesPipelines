namespace FiapCloudGamesAPI.EventSourcing.Eventos.Jogo;

public record JogoDeletado(Guid JogoId) : BaseEvent(JogoId);