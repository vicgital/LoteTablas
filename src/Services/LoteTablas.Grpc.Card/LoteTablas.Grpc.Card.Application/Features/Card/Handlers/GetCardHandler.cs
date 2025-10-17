using LoteTablas.Application.Contracts.Cache;
using LoteTablas.Application.Contracts.Configuration;
using LoteTablas.Grpc.Card.Application.Contracts.Persistence;
using LoteTablas.Grpc.Card.Application.Features.Card.DTO;
using LoteTablas.Grpc.Card.Application.Features.Card.Queries;
using MediatR;

namespace LoteTablas.Grpc.Card.Application.Features.Card.Handlers
{
    public class GetCardHandler(
        ICardRepository repository,
        IAppConfigurationManager appConfigurationManager,
        IMemoryCacheManager memoryCacheManager) : IRequestHandler<Queries.GetCardQuery, DTO.CardDto?>
    {
        private readonly ICardRepository _repository = repository;
        private readonly IMemoryCacheManager _memoryCacheManager = memoryCacheManager;
        private readonly string storageAccountBaseUrl = appConfigurationManager.GetValue("STORAGE_ACCOUNT_BASE_URL");

        public async Task<CardDto?> Handle(GetCardQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"Card_{request.CardId}";
            var result = _memoryCacheManager.Get<CardDto>(cacheKey);
            if (result == null)
            {
                var dbCard = await _repository.GetCard(request.CardId);
                if (dbCard == null) return null;
                
                result = CardDto.FromEntity(dbCard, storageAccountBaseUrl);
                _memoryCacheManager.Add<CardDto>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_memoryCacheManager.GetCacheDuration()));
            }
            return result;
        }
        
    }
}
