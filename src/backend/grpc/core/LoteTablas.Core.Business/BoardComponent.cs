using AutoMapper;
using LoteTablas.Core.Business.Interfaces;
using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Core.Models;
using LoteTablas.Framework.Common.Cache;
using LoteTablas.Framework.Common.Configuration;
using System.Text;

namespace LoteTablas.Core.Business
{
    public class BoardComponent : IBoardComponent
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IPdfGeneratorComponent _pdfGeneratorComponent;
        private readonly ICardRepository _cardRepository;
        private readonly IInMemoryCache _inMemoryCache;
        private readonly double _cacheDuration;
        private readonly IAppConfiguration _config;
        private readonly IMapper _mapper;


        public BoardComponent(IAppConfiguration config, IInMemoryCache inMemoryCache, IMapper mapper, IBoardRepository boardRepository, ICardRepository cardRepository, IPdfGeneratorComponent pdfGeneratorComponent)
        {
            _boardRepository = boardRepository;
            _pdfGeneratorComponent = pdfGeneratorComponent;
            _mapper = mapper;
            _cardRepository = cardRepository;
            _inMemoryCache = inMemoryCache;
            _cacheDuration = double.Parse(config.GetValue("CACHE_DURATION", "10"));
            _config = config;
        }

        public async Task<List<Models.BoardSize>> GetBoardSizes()
        {
            var cacheKey = $"BoardSizes";
            var result = _inMemoryCache.Get<List<Models.BoardSize>>(cacheKey);

            if (result == null)
            {
                var boardSizes = await _boardRepository.GetBoardSizes();
                result = _mapper.Map<List<Models.DAO.BoardSize>, List<Models.BoardSize>>(boardSizes);
                if (result.Count > 0)
                {
                    _inMemoryCache.Add(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));
                }

            }

            return result;
        }

        public async Task<Models.Board?> SaveBoard(Models.Board board)
        {
            var boardDAO = _mapper.Map<Models.Board, Models.DAO.Board>(board);
            var resultDAO = await _boardRepository.SaveBoard(boardDAO);
            if (resultDAO != null)
            {
                var cacheKey = $"UserBoards_{board.OwnerUserID}";
                _inMemoryCache.Remove(cacheKey);
                return _mapper.Map<Models.DAO.Board, Models.Board>(resultDAO);
            }

            return null;
        }


        public async Task<bool> DeleteBoard(int boardID)
        {
            var result = await _boardRepository.DeleteBoard(boardID);
            if (result)
            {
                var cacheKey = $"FullBoard_{boardID}";
                _inMemoryCache.Remove(cacheKey);
            }

            return result;
        }


        public async Task<List<Models.Board>> GetBoardsByUserID(int userID)
        {
            var cacheKey = $"UserBoards_{userID}";
            var result = _inMemoryCache.Get<List<Models.Board>>(cacheKey);

            if (result == null)
            {
                var boards = await _boardRepository.GetBoardsByUserID(userID);
                if (boards.Count > 0)
                {
                    result = _mapper.Map<List<Models.DAO.Board>, List<Models.Board>>(boards);
                    _inMemoryCache.Add(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));
                }
            }


            return [];

        }


        public async Task<Models.Board?> GetFullBoardByBoardID(int boardID)
        {
            var cacheKey = $"FullBoard_{boardID}";
            var result = _inMemoryCache.Get<Models.Board>(cacheKey);

            if (result == null)
            {
                var boardDAO = await _boardRepository.GetBoard(boardID);
                if (boardDAO is null)
                    return null;


                result = _mapper.Map<Models.DAO.Board, Models.Board>(boardDAO);
                if (result == null)
                    return null;
                else
                {
                    var boardCards = await _boardRepository.GetBoardCardsByBoardID(boardID);
                    if (boardCards.Count != 0)
                    {
                        var cardIds = boardCards.Select(x => x.CardID).ToList();
                        var cards = await _cardRepository.GetCards(cardIds);

                        result.BoardCards = (from bc in boardCards
                                             join c in cards on bc.CardID equals c.CardID
                                             select new Models.BoardCard
                                             {
                                                 CardID = bc.CardID,
                                                 Ordinal = bc.Ordinal,
                                                 //CreatorUserID = bc.CreatorUserID,
                                                 Description = c.Description,
                                                 ImageBigPath = c.ImageBigPath,
                                                 ImageSmallPath = c.ImageSmallPath,
                                                 ImageMediumPath = c.ImageMediumPath,
                                                 Name = c.Name,

                                             }).OrderBy(e => e.Ordinal).ToList();
                    }

                    _inMemoryCache.Add(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));

                    return result;
                }
            }

            return result;


        }

        public async Task<bool> SaveBoardCards(int boardID, List<Models.BoardCard> boardCards)
        {
            var boardCardsDAO = _mapper.Map<List<Models.BoardCard>, List<Models.DAO.BoardCard>>(boardCards);

            var result = await _boardRepository.SaveBoardCards(boardID, boardCardsDAO);
            if (result)
            {
                var cacheKey = $"FullBoard_{boardID}";
                _inMemoryCache.Remove(cacheKey);
            }

            return result;

        }

        public async Task<byte[]> GetBoardDocument(int boardId)
        {
            var boardHtml = await GetBoardDocumentHtml(boardId);
            var html = GetInitialHtml().Replace("{{BoardHtml}}", boardHtml);

            return _pdfGeneratorComponent.ConvertHtmlToPDF(html);

        }


        public async Task<byte[]> GetBoardsDocument(List<int> boardIds)
        {

            var boardsHtml = new StringBuilder();
            foreach (var boardId in boardIds)
            {
                var boardHtml = await GetBoardDocumentHtml(boardId);
                boardsHtml.Append(boardHtml);
            }

            var html = GetInitialHtml().Replace("{{BoardHtml}}", boardsHtml.ToString());

            return _pdfGeneratorComponent.ConvertHtmlToPDF(html);
        }

        public async Task<byte[]> GetUserBoardsLiteDocument(List<BoardLite> userBoards)
        {
            var boardsHtml = new StringBuilder();
            foreach (var board in userBoards)
            {
                var boardHtml = await GetBoardDocumentHtml(board);
                boardsHtml.Append(boardHtml);
            }

            var html = GetInitialHtml().Replace("{{BoardHtml}}", boardsHtml.ToString());

            return _pdfGeneratorComponent.ConvertHtmlToPDF(html);
        }

        private async Task<Models.BoardSize?> GetBoardSize(int boardSizeID)
        {

            var boardSizeDAO = await _boardRepository.GetBoardSize(boardSizeID) ?? throw new KeyNotFoundException($"BoardSize with id {boardSizeID} not found");
            return _mapper.Map<Models.DAO.BoardSize, Models.BoardSize>(boardSizeDAO);
        }


        private async Task<string> GetBoardDocumentHtml(int boardId)
        {

            var board = (await GetFullBoardByBoardID(boardId)) ?? throw new KeyNotFoundException($"Board with id {boardId} not found");
            var boardSize = await GetBoardSize(board.BoardSizeID) ?? throw new KeyNotFoundException($"BoardSize with id {board.BoardSizeID} not found"); ;

            var xSize = boardSize.XSize;
            var ySize = boardSize.YSize;
            List<Models.DocumentBoardCard> boardCards = MapDocumentCards(board.BoardCards);

            return BuildBoardHtml(xSize, ySize, boardCards);
        }

        private async Task<string> GetBoardDocumentHtml(BoardLite board)
        {
            List<Models.DocumentBoardCard> boardCards = await MapDocumentCards(board.BoardCards);
            return BuildBoardHtml(4, 4, boardCards);
        }

        private static string BuildBoardHtml(int xSize, int ySize, List<Models.DocumentBoardCard> boardCards)
        {


            var boardHtml = GetBoardHtml();

            var boardRowsHtml = new StringBuilder();

            var ordinal = 0;
            for (int x = 1; x <= xSize; x++)
            {
                var rowHtml = GetBoardRowHtml();

                var boardCellsHtml = new StringBuilder();

                for (int y = 1; y <= ySize; y++)
                {
                    ordinal++;
                    var card = boardCards.FirstOrDefault(e => e.Ordinal == ordinal);

                    var cardHtml = GetBoardCellHtml(card?.ImageUrl ?? string.Empty);

                    boardCellsHtml.Append(cardHtml);

                }

                rowHtml = rowHtml.Replace("{{BoardCellsHtml}}", boardCellsHtml.ToString());

                boardRowsHtml.Append(rowHtml);

            }


            boardHtml = boardHtml.Replace("{{BoardRowsHtml}}", boardRowsHtml.ToString());

            return boardHtml;


        }

        private List<Models.DocumentBoardCard> MapDocumentCards(List<Models.BoardCard> boardCards)
        {
            var baseUrl = _config.GetValue("STORAGE_ACCOUNT_BASE_URL");

            return boardCards.Select(e => new Models.DocumentBoardCard
            {
                Ordinal = e.Ordinal,
                ImageUrl = $"{baseUrl}{e.ImageMediumPath}",
            }).ToList();

        }

        private async Task<List<DocumentBoardCard>> MapDocumentCards(List<BoardCardLite> boardCards)
        {
            var baseUrl = _config.GetValue("STORAGE_ACCOUNT_BASE_URL");
            var cardIds = boardCards.Select(e => e.CardID).ToList();
            var cards = await _cardRepository.GetCards(cardIds);

            return boardCards.Select(e => new DocumentBoardCard
            {
                Ordinal = e.Ordinal,
                ImageUrl = baseUrl + cards.FirstOrDefault(c => c.CardID == e.CardID)?.ImageSmallPath ?? string.Empty,
            }).ToList();

        }

        private static string GetInitialHtml()
        {
            return @"

                <!DOCTYPE html>
                <html>
                  <head>
                    <title>LoteTablas Board</title>
                    <style>
                      table {
                        border-collapse: collapse;
                      }

                      th,
                      td {
                        border: 1px solid black;
                        text-align: center;
                      }

                      td div {
                        width: 100px;
                        height: 160px;
                        background-size: cover;
                        background-repeat: no-repeat;
                      }
	  
	                  .board {
		                display: inline-block;
		                padding-left: 40px;
		                padding-top: 40px;
		
	                  }
	  
                    </style>
                  </head>
                  <body>
                        {{BoardHtml}}
                  </body>
                </html>


            ";
        }

        private static string GetBoardHtml()
        {
            return @"

                <div class=""board"">
	
		            <table>     
                        {{BoardRowsHtml}}       
                    </table>
	
	            </div>

            ";
        }


        private static string GetBoardRowHtml()
        {
            return @"
                    <tr>
                        {{BoardCellsHtml}}
                    </tr>
            ";
        }

        private static string GetBoardCellHtml(string cellBackgroundUrl)
        {
            return @$"
                        <td>
                          <div style=""background-image: url('{cellBackgroundUrl}');""></div>
                        </td>
            ";
        }
    }
}
