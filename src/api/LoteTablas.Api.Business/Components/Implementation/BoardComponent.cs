using AutoMapper;
using LoteTablas.Api.Business.Components.Definition;
using LoteTablas.Api.Data.Repositories.Definition;
using LoteTablas.Api.Models;
using LoteTablas.Core.Service.Definition;
using Microsoft.Extensions.Logging;

namespace LoteTablas.Api.Business.Components.Implementation
{
    public class BoardComponent(IMapper mapper,
        ILogger<BoardComponent> logger,
        IBoardRepository boardRepository,
        ICardRepository cardRepository
        ) : IBoardComponent
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<BoardComponent> _logger = logger;
        private readonly IBoardRepository _boardRepository = boardRepository;
        private readonly ICardRepository _cardRepository = cardRepository;

        public Task<bool> DeleteBoard(int boardID)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> DownloadBoards(Models.MVP.UserBoardsRequest request)
        {
            var documentRequest = new UserBoardsLiteDocumentRequest();

            documentRequest.UserBoards.AddRange(_mapper.Map<List<Models.MVP.Board>, List<BoardLiteModel>>(request.Boards));

            var document = await _boardRepository.GetUserBoardsDocument(documentRequest);
            return document;
        }

        public Task<byte[]> GetBoardDocument(int boardID)
        {
            return _boardRepository.GetBoardDocument(boardID);
        }

        public Task<byte[]> GetBoardDocuments(List<int> boardIDs)
        {
            return _boardRepository.GetBoardDocuments(boardIDs);
        }

        public Task<List<Board>> GetBoardsByUserID(int userID)
        {
            throw new NotImplementedException();
        }

        public Task<List<BoardSize>> GetBoardSizes()
        {
            throw new NotImplementedException();
        }

        public Task<Board> GetFullBoardByBoardID(int boardID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveBoardCards(int boardID, List<BoardCard> boardCards)
        {
            throw new NotImplementedException();
        }
    }
}
