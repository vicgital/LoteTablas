
namespace LoteTablas.Grpc.Lottery.Application.Features.LotteryType.DTO
{
    public class LotteryTypeDto
    {
        public string LotteryTypeId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public static LotteryTypeDto FromEntity(Domain.Entities.LotteryType entity)
        {
            return new LotteryTypeDto
            {
                LotteryTypeId = entity.Id,
                Name = entity.Name ?? string.Empty,
                Description = entity.Description ?? string.Empty,
                Code = entity.Code ?? string.Empty
            };
        }
    }
}
