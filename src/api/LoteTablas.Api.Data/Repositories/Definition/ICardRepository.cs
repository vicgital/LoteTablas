using LoteTablas.Core.Service.Definition;

namespace LoteTablas.Api.Data.Repositories.Definition
{
    public interface ICardRepository
    {
        Task<CardModel> GetCard(int cardID);
        Task<List<CardModel>> GetCards(List<int> cardIds);
    }
}
