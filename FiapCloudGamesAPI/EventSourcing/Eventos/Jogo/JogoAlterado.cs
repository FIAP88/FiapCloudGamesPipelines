namespace FiapCloudGamesAPI.EventSourcing.Eventos.Jogo;

public record JogoAlterado(Guid JogoId) : BaseEvent(JogoId);