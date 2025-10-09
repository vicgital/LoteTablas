namespace LoteTablas.Grpc.Lottery.Domain.Entities
{
    public class LotteryType
    {
        public int LotteryTypeID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
