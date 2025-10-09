using Grpc.Core;
using LoteTablas.Api.Data.Repositories.Definition;
using LoteTablas.Core.Service.Definition;
using System.Reflection.Metadata;
using static LoteTablas.Core.Service.Definition.Core;

namespace LoteTablas.Api.Data.Repositories.Implementation
{
    public class BoardRepository(CoreClient coreClient) : IBoardRepository
    {
        protected readonly CoreClient _coreClient = coreClient;

        public async Task<bool> DeleteBoard(int boardID)
        {
            var reply = await _coreClient.DeleteBoardAsync(new DeleteBoardRequest
            {
                BoardID = boardID
            });

            return reply.Success;

        }

        public Task<byte[]> GetBoardDocument(int boardID)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetBoardDocuments(List<int> boardIDs)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> GetUserBoardsDocument(UserBoardsLiteDocumentRequest request) 
        {
            List<byte> fileBytes = [];
            var streamRequest = _coreClient.GetUserBoardsLiteDocumentStream(request, deadline: DateTime.UtcNow.AddSeconds(10));

            while (await streamRequest.ResponseStream.MoveNext())
            {
                fileBytes.AddRange(streamRequest.ResponseStream.Current.Data.ToByteArray());
            }

            return [.. fileBytes];
        }



        public Task<List<BoardModel>> GetBoardsByUserID(int userID)
        {
            throw new NotImplementedException();
        }

        public Task<List<BoardSizeModel>> GetBoardSizes()
        {
            throw new NotImplementedException();
        }

        public Task<BoardModel> GetFullBoardByBoardID(int boardID)
        {
            throw new NotImplementedException();
        }

        public Task<BoardModel> SaveBoard(SaveBoardRequest board)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveBoardCards(int boardID, List<BoardCardModel> boardCards)
        {
            throw new NotImplementedException();
        }
    }
}
