using LoteTablas.Api.Data.Repositories.Definition;
using LoteTablas.Core.Service.Definition;
using static LoteTablas.Core.Service.Definition.Core;

namespace LoteTablas.Api.Data.Repositories.Implementation
{
    public class CardRepository(CoreClient coreClient) : ICardRepository
    {
        protected readonly CoreClient _coreClient = coreClient;

        public async Task<CardModel> GetCard(int cardID) =>
            await _coreClient.GetCardAsync(new GetCardRequest { CardID = cardID });


        public async Task<List<CardModel>> GetCards(List<int> cardIds)
        {

            var request = new GetCardsRequest();
            request.CardIDs.AddRange(cardIds);
            var reply = await _coreClient.GetCardsAsync(request);
            return [.. reply.Cards];

        }
    }
}
