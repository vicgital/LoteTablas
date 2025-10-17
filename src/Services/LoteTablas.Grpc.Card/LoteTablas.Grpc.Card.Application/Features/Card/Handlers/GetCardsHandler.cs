using LoteTablas.Application.Contracts.Configuration;
using LoteTablas.Grpc.Card.Application.Contracts.Persistence;
using LoteTablas.Grpc.Card.Application.Features.Card.DTO;
using LoteTablas.Grpc.Card.Application.Features.Card.Queries;
using MediatR;

namespace LoteTablas.Grpc.Card.Application.Features.Card.Handlers
{
    public class GetCardsHandler(
        ICardRepository repository,
        IAppConfigurationManager appConfigurationManager
        ) : IRequestHandler<Queries.GetCardsQuery, List<DTO.CardDto>>
    {
        private readonly ICardRepository _repository = repository;
        private readonly string storageAccountBaseUrl = appConfigurationManager.GetValue("STORAGE_ACCOUNT_BASE_URL");

        public async Task<List<CardDto>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            var dbCards = await _repository.GetCards(request.CardIds);
            var result = dbCards.Select(card => CardDto.FromEntity(card, storageAccountBaseUrl)).ToList();
            return result;
        }
    }
}
