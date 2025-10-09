namespace LoteTablas.Grpc.Lottery.Service
{
    public class LotteryService(ILogger<LotteryService> logger) : Definitions.Lottery.LotteryBase
    {
        private readonly ILogger<LotteryService> _logger = logger;
        
    }
}
