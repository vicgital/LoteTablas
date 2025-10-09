using LoteTablas.Blazor.UI.Data.Repositories.Definition;
using Microsoft.Extensions.Logging;

namespace LoteTablas.Blazor.UI.Data.Repositories.Implementation.Test
{
    public class BoardRepository(IHttpClientFactory httpClientFactory, ILogger<BoardRepository> logger) : IBoardRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentException("httpClientFactory is null");
        private readonly ILogger<BoardRepository> _logger = logger ?? throw new ArgumentException("logger is null");

        public async Task<Stream> DownloadBoards(List<Models.Board> boards)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient("api");
                var response = await client.GetByteArrayAsync($"data/testboard.pdf");
                return new MemoryStream(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DownloadBoards()");
                throw;
            }
        }
    }
}
