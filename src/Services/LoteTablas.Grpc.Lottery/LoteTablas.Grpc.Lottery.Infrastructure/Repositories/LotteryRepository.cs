using LoteTablas.Grpc.Lottery.Application.Contracts.Persistence;
using LoteTablas.Grpc.Lottery.Domain.Entities;
using MongoDB.Driver;

namespace LoteTablas.Grpc.Lottery.Infrastructure.Repositories
{
    public class LotteryRepository(IMongoDatabase mongoDatabase) : ILotteryRepository
    {
        private readonly IMongoCollection<Domain.Entities.Lottery> _lotteries = mongoDatabase.GetCollection<Domain.Entities.Lottery>("lotteries");
        private readonly IMongoCollection<Domain.Entities.LotteryType> _lotteryTypes = mongoDatabase.GetCollection<Domain.Entities.LotteryType>("lotteryTypes");
        private readonly IMongoCollection<Domain.Entities.LotteryCards> _lotteryCards = mongoDatabase.GetCollection<Domain.Entities.LotteryCards>("lotteryCards");
        private readonly IMongoCollection<Domain.Entities.Card> _cards = mongoDatabase.GetCollection<Domain.Entities.Card>("cards");

        public async Task<List<Domain.Entities.Lottery>> GetFreeLotteries()
        {
            var result = await _lotteries.Find(e => e.OwnerUserId == null && e.Enabled).ToListAsync();
            return [.. result];
        }

        public async Task<List<Domain.Entities.Lottery>> GetLotteriesByLotteryType(string lotteryTypeId)
        {


            var result = await _lotteries.Find(e =>
                e.OwnerUserId == null &&
                e.Enabled &&
                e.LotteryTypeId != null &&
                e.LotteryTypeId.Equals(lotteryTypeId)).ToListAsync();

            return result;

        }

        public async Task<List<Domain.Entities.Lottery>> GetLotteriesByUserId(string userId)
        {

            var result = await _lotteries.Find(e =>
                e.OwnerUserId != null &&
                e.OwnerUserId.Equals(userId) &&
                e.Enabled).ToListAsync();

            return [.. result];

        }

        public async Task<Domain.Entities.Lottery?> GetLottery(string lotteryId)
        {

            var result = await _lotteries.Find(e =>
                e.Id == lotteryId &&
                e.Enabled).SingleOrDefaultAsync();

            return result;
        }

        public async Task<List<LotteryType>> GetLotteryTypes()
        {
            var result = await _lotteryTypes.Find(e => true).ToListAsync();
            return [.. result];
        }

        public async Task<(LotteryCards?, List<Card>)> GetLotteryAndCards(string lotteryId)
        {
            var pipeline = _lotteryCards.Aggregate()
            .Match(l => l.LotteryId.Equals(lotteryId))
            .Lookup(
                foreignCollection: _cards,
                localField: l => l.Cards.Select(bc => bc.CardId),
                foreignField: c => c.Id,
                @as: (LotteryCardsDetails l) => l.CardDetails
            );

            var result = await pipeline.FirstOrDefaultAsync();

            if (result == null)
                return (null, []);

            return (result, result.CardDetails);

        }

        private class LotteryCardsDetails : Domain.Entities.LotteryCards
        {

            public List<Card> CardDetails { get; set; } = [];
        }
    }
}
