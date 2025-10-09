namespace LoteTablas.Api.Business.Components.Definition
{
    public interface ICardComponent
    {
        Task<Models.Card?> GetCard(int cardID);
        Task<List<Models.Card>> GetCards(List<int> cardIds);
    }
}
