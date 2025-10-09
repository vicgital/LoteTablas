using LoteTablas.Blazor.UI.Data.Repositories.Definition;
using LoteTablas.Blazor.UI.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LoteTablas.Blazor.UI.Data.Repositories.Implementation
{
    public class LotteryRepository(IHttpClientFactory httpClientFactory, ILogger<LotteryRepository> logger) : ILotteryRepository
    {

        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentException("httpClientFactory is null");
        private readonly ILogger<LotteryRepository> _logger = logger ?? throw new ArgumentException("logger is null");

        public async Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID)
        {

            try
            {
                List<LotteryCard> lotteryCards = [];

                if (lotteryID > 0)
                {
                    using var client = _httpClientFactory.CreateClient("api");
                    var response = await client.GetAsync($"v1/lottery/cards/{lotteryID}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        lotteryCards = JsonSerializer.Deserialize<List<LotteryCard>>(content) ?? [];
                    }
                }

                return lotteryCards;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLotteryCards()");
                throw;
            }
        }


    }
}
