namespace LoteTablas.Core.Business.Interfaces
{
    public interface ICardComponent
    {
        Task<Models.Card?> GetCard(int cardID);
        Task<List<Models.Card>> GetCards(List<int> cardIds);
    }
}
