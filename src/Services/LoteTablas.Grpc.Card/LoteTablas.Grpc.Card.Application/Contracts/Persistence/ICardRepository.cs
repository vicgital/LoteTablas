using LoteTablas.Grpc.Card.Domain.Entities;

namespace LoteTablas.Grpc.Card.Application.Contracts.Persistence
{
    public interface ICardRepository
    {
        Task<Domain.Entities.Card> GetCard(string cardId);
        Task<List<Domain.Entities.Card>> GetCards(List<string> cardIds);
    }
}
