namespace LoteTablas.Grpc.Lottery.Application.Features.Lottery.DTO
{
    public class LotteryDto
    {
        public string LotteryId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LotteryTypeId { get; set; } = string.Empty;
        public string LotteryType { get; set; } = string.Empty;
        public string OwnerUserId { get; set; } = string.Empty;

        public static LotteryDto FromEntity(Domain.Entities.Lottery entity)
        {
            return new LotteryDto
            {
                LotteryId = entity.Id,
                Name = entity.Name ?? string.Empty,
                Description = entity.Description ?? string.Empty,
                LotteryTypeId = entity.LotteryTypeId ?? string.Empty,
                LotteryType = entity.LotteryType ?? string.Empty,
                OwnerUserId = entity.OwnerUserId ?? string.Empty
            };
        }



    }
}
