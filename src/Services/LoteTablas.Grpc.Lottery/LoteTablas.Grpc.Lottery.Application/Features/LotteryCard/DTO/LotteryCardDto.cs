using System;

namespace LoteTablas.Grpc.Lottery.Application.Features.LotteryCard.DTO
{
    public class LotteryCardDto
    {
        public string CardID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int Ordinal { get; set; }

        internal static LotteryCardDto FromEntity(Domain.Entities.LotteryCard e, List<Domain.Entities.Card> cards, string storageAccountBaseUrl)
        {
            var card = cards.FirstOrDefault(c => c.Id.Equals(e.CardId.ToString()));
            if (card != null)
            {
                return new LotteryCardDto
                {
                    CardID = card.Id ?? string.Empty,
                    Name = card.Name ?? string.Empty,
                    Description = card.Description ?? string.Empty,
                    ImagePath = string.IsNullOrEmpty(card.ImagePath) ? string.Empty : $"{storageAccountBaseUrl}{card.ImagePath}",
                    Ordinal = e.Ordinal
                };
            }
            else
                return new();
        }
    }
}
