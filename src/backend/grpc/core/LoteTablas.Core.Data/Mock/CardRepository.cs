using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Core.Models.DAO;

namespace LoteTablas.Core.Data.Mock;

public class CardRepository : ICardRepository
{
    public Task<Card?> GetCard(int cardID)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Card>> GetCards(List<int> cardIds)
    {
        await Task.Delay(1000);
        return Mock.Data.Cards.Where(c => cardIds.Contains(c.CardID)).ToList();
    }
}
