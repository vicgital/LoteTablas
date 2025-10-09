namespace LoteTablas.Core.Data.Interfaces
{
    public interface ICardRepository
    {
        Task<Models.DAO.Card?> GetCard(int cardID);
        Task<List<Models.DAO.Card>> GetCards(List<int> cardIds);
    }
}
