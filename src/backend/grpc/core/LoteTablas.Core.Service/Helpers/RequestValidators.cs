using Grpc.Core;
using LoteTablas.Core.Service.Definition;

namespace LoteTablas.Core.Service.Helpers
{
    public static class RequestValidators
    {
        public static void ValidateGetCardsRequest(GetCardsRequest request)
        {
            if (request == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Invalid request"));

            if (request.CardIDs.Count == 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"CardIDs is required"));


        }

        public static void ValidateGetCardRequest(GetCardRequest request)
        {
            if (request == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Invalid request"));

            if (request.CardID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Invalid CardID"));
        }

        public static void ValidateSaveBoardRequest(SaveBoardRequest request)
        {
            if (request.LotteryID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid LotteryId"));

            if (request.BoardSizeId <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid BoardSizeId"));

        }

        public static void ValidateDeleteBoardRequest(DeleteBoardRequest request)
        {
            if (request.BoardID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid BoardId"));
        }

        public static void ValidateGetBoardsByUserIDRequest(UserIDRequest request)
        {
            if (request.UserID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid UserID"));
        }

        public static void ValidateGetFullBoardByBoardIDRequest(BoardIDRequest request)
        {
            if (request.BoardID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid BoardID"));

        }

        public static void ValidateSaveBoardCardsRequest(SaveBoardCardsRequest request)
        {
            if (request.BoardID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid BoardID"));

            if (request.BoardCards == null || request.BoardCards.Count == 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid BoardCards"));

        }

        public static void ValidateGetBoardDocumentStreamRequest(BoardDocumentRequest request)
        {
            if (request.BoardID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid BoardID"));
        }

        public static void ValidateGetBoardDocumentsStreamRequest(BoardDocumentsRequest request)
        {
            if (request.BoardIDs == null || request.BoardIDs.Count == 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid BoardIDs"));
        }

        internal static void ValidateUserBoardsLiteDocumentRequest(UserBoardsLiteDocumentRequest request)
        {
            if (request.UserBoards.Count <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Empty UserBoards"));

            if (request.UserBoards.Any(e => e.BoardCards.Count == 0))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Empty Board"));

        }
    }
}
