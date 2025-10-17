using LoteTablas.Grpc.Card.Application.Contracts.Persistence;
using MongoDB.Driver;

namespace LoteTablas.Grpc.Card.Infrastructure.Repositories
{
    public class CardRepository(IMongoDatabase mongoDatabase) : ICardRepository
    {

        private readonly IMongoCollection<Domain.Entities.Card> _cards = mongoDatabase.GetCollection<Domain.Entities.Card>("cards");

        public async Task<Domain.Entities.Card> GetCard(string cardId)
        {
            return await _cards.Find(e => e.Id == cardId && e.Enabled).FirstOrDefaultAsync();
        }

        public Task<List<Domain.Entities.Card>> GetCards(List<string> cardIds)
        {
            return _cards.Find(e => cardIds.Contains(e.Id) && e.Enabled).ToListAsync();
        }
    }
}
