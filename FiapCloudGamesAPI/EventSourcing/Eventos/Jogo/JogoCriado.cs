namespace FiapCloudGamesAPI.EventSourcing.Eventos.Jogo;

public record JogoCriado(Guid JogoId) : BaseEvent(JogoId);