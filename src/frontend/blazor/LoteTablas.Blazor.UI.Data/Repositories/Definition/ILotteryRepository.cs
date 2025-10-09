using LoteTablas.Blazor.UI.Models;

namespace LoteTablas.Blazor.UI.Data.Repositories.Definition
{
    public interface ILotteryRepository
    {
        Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID);
    }
}
