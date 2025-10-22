using Grpc.Core;
using LoteTablas.Grpc.Definitions;

namespace LoteTablas.Grpc.Board.Service.Helpers
{
    public static class RequestValidator
    {
        internal static void ValidateBoardDocumentRequest(BoardDocumentRequest request)
        {

            if (request.Board == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Board is null"));

            if (request.Board.BoardCards.Count <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Empty Cards in Board"));

        }

        internal static void ValidateBoardDocumentsRequest(BoardDocumentsRequest request)
        {
            if (request.Boards == null || request.Boards.Count <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Boards in request are empty"));

            foreach (var board in request.Boards)
            {
                if (board.BoardCards.Count <= 0)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Empty Cards in Board"));
            }

        }
    }
}
