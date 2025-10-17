using LoteTablas.Application.Contracts.Cache;
using LoteTablas.Application.Contracts.Configuration;
using LoteTablas.Grpc.Lottery.Application.Contracts.Persistence;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryCard.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryCard.Queries;
using MediatR;

namespace LoteTablas.Grpc.Lottery.Application.Features.LotteryCard.Handlers
{
    public class GetLotteryCardsByLotteryIdHandler(
        ILotteryRepository repository,
        IAppConfigurationManager appConfigurationManager,
        IMemoryCacheManager memoryCacheManager) : IRequestHandler<GetLotteryCardsByLotteryIdQuery, List<LotteryCardDto>>
    {
        private readonly ILotteryRepository repository = repository;
        private readonly IMemoryCacheManager memoryCacheManager = memoryCacheManager;
        private readonly string storageAccountBaseUrl = appConfigurationManager.GetValue("STORAGE_ACCOUNT_BASE_URL");




        public async Task<List<LotteryCardDto>> Handle(GetLotteryCardsByLotteryIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"LotteryCards_Lottery_{request.LotteryId}";
            var result = memoryCacheManager.Get<List<LotteryCardDto>>(cacheKey);
            if (result == null)
            {
                (var lotteryCards, var cards) = await repository.GetLotteryAndCards(request.LotteryId);
                result = lotteryCards == null ? [] : 
                    [.. lotteryCards.Cards
                    .Select(e => LotteryCardDto.FromEntity(e, cards, storageAccountBaseUrl))
                    .Where(e => !string.IsNullOrEmpty(e.CardID))
                    .OrderBy(e => e.Ordinal)];

                if (result.Count > 0)
                {
                    memoryCacheManager.Add<List<LotteryCardDto>>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(memoryCacheManager.GetCacheDuration()));
                }
            }
            return result;
        }
    }
}
