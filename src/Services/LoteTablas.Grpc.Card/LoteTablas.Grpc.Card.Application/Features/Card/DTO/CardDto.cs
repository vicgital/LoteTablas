namespace LoteTablas.Grpc.Card.Application.Features.Card.DTO
{
    public class CardDto
    {
        public string CardId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string CreatorUserId { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;

        public static CardDto FromEntity(Domain.Entities.Card entity, string storageAccountBaseUrl)
        {
            return new CardDto
            {
                CardId = entity.Id,
                Name = entity.Name ?? string.Empty,
                Description = entity.Description ?? string.Empty,
                ImagePath = string.IsNullOrEmpty(entity.ImagePath) ? string.Empty : $"{storageAccountBaseUrl}{entity.ImagePath}",
                CreatorUserId = entity.CreatorUserId ?? string.Empty,
                CreatedAt = entity.CreatedAt.ToString("o")

            };
        }

    }
}
