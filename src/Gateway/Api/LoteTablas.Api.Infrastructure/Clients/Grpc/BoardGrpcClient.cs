using LoteTablas.Api.Application.Contracts.Clients.Grpc;
using LoteTablas.Api.Domain;
using LoteTablas.Grpc.Definitions;
using static LoteTablas.Grpc.Definitions.Board;

namespace LoteTablas.Api.Infrastructure.Clients.Grpc
{
    public class BoardGrpcClient(BoardClient grpcClient) : IBoardGrpcClient
    {
        private readonly BoardClient _grpcClient = grpcClient;

        public async Task<byte[]> GetBoardDocuments(BoardDocuments boardDocuments)
        {


            var request = BuildGrpcBoardDocumentsRequest(boardDocuments);
            var streamRequest = _grpcClient.GetBoardDocumentsStream(request, deadline: DateTime.UtcNow.AddSeconds(10));
            List<byte> fileBytes = [];

            while (await streamRequest.ResponseStream.MoveNext(CancellationToken.None))
            {
                fileBytes.AddRange(streamRequest.ResponseStream.Current.Data.ToByteArray());
            }

            return [.. fileBytes];

        }

        private static BoardDocumentsRequest BuildGrpcBoardDocumentsRequest(BoardDocuments boardDocuments)
        {
            BoardDocumentsRequest request = new();

            foreach (var board in boardDocuments.Boards)
            {
                BoardDocumentModel boardModel = new();
                foreach (var card in board.BoardCards)
                {
                    BoardDocumentCardModel cardModel = new()
                    {
                         CardId = card.CardId,
                         Ordinal = card.Ordinal
                    };
                    boardModel.BoardCards.Add(cardModel);
                }

                request.Boards.Add(boardModel);
            }

            return request;

        }
    }
}
