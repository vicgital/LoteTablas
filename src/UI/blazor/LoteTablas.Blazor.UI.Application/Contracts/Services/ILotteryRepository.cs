namespace LoteTablas.Blazor.UI.Application.Contracts.Repositories
{
    public interface ILotteryRepository
    {
        Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID);
    }
}
