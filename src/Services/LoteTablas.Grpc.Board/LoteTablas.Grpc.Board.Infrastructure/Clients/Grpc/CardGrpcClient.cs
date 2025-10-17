using LoteTablas.Grpc.Board.Application.Contracts.Clients.Grpc;

namespace LoteTablas.Grpc.Board.Infrastructure.Clients.Grpc
{
    public class CardGrpcClient(Definitions.Card.CardClient cardClient) : ICardGrpcClient
    {
        private readonly Definitions.Card.CardClient _cardClient = cardClient;

        public async Task<List<Domain.Entities.Card>> GetCards(List<string> cardIds)
        {
            var request = new Definitions.GetCardsRequest();
            request.CardIds.AddRange(cardIds);

            var reply = await _cardClient.GetCardsAsync(request);

            return reply.Cards.Select(c => new Domain.Entities.Card
            {
                Name = c.Name,
                ImagePath = c.ImagePath
            }).ToList();



        }
    }
}
