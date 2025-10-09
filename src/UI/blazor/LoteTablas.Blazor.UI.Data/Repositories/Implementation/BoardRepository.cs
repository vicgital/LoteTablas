using LoteTablas.Blazor.UI.Data.Repositories.Definition;
using LoteTablas.Blazor.UI.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace LoteTablas.Blazor.UI.Data.Repositories.Implementation
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
                JsonContent httpContent = JsonContent.Create(MapDownloadBoardsRequest(boards));
                var response = await client.PostAsync($"v1/board/document/download", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    return content;
                }
                else
                    throw new Exception($"Error downloading boards, Status Code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DownloadBoards()");
                throw;
            }
        }

        private static DownloadBoardsRequest MapDownloadBoardsRequest(List<Board> boards)
        {
            return new DownloadBoardsRequest
            {
                Boards = boards.Select(board => new UserBoard
                {
                    BoardID = board.BoardId,
                    BoardCards = board.BoardCards.Select(card => new BoardCard
                    {
                        CardID = card.CardID,
                        Ordinal = card.Ordinal
                    }).ToList()
                }).ToList()
            };
        }
    }
}
