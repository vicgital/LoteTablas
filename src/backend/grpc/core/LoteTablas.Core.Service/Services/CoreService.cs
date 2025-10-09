using AutoMapper;
using Google.Protobuf;
using Grpc.Core;
using LoteTablas.Core.Business.Interfaces;
using LoteTablas.Core.Models;
using LoteTablas.Core.Service.Definition;
using LoteTablas.Core.Service.Helpers;
using LoteTablas.Framework.Common.Configuration;

namespace LoteTablas.Core.Service.Services
{
    public class CoreService(
        ILogger<CoreService> logger,
        IMapper mapper,
        IAppConfiguration appConfiguration,
        ILotteryComponent lotteryComponent,
        ICardComponent cardComponent,
        IBoardComponent boardComponent) : Definition.Core.CoreBase
    {
        private readonly ILogger<CoreService> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IAppConfiguration _appConfiguration = appConfiguration;
        private readonly ILotteryComponent _lotteryComponent = lotteryComponent;
        private readonly ICardComponent _cardComponent = cardComponent;
        private readonly IBoardComponent _boardComponent = boardComponent;

        #region Lottery


        public async override Task<LotteriesReply> GetLotteriesByUserId(UserIdRequest request, ServerCallContext context)
        {

            if (request.UserID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "UserId is required"));

            try
            {
                LotteriesReply reply = new();
                List<Models.Lottery> result = await _lotteryComponent.GetLotteriesByUserId(request.UserID);

                if (result != null)
                    reply.Lotteries.AddRange(_mapper.Map<List<Models.Lottery>, List<LotteryModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLotteriesByUserId({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteriesByUserId() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteriesReply> GetFreeLotteries(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                LotteriesReply reply = new();
                List<Models.Lottery> result = await _lotteryComponent.GetFreeLotteries();

                if (result != null)
                    reply.Lotteries.AddRange(_mapper.Map<List<Models.Lottery>, List<LotteryModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetFreeLotteries({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetFreeLotteries() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteriesReply> GetLotteriesByLotteryType(LotteryTypeIDRequest request, ServerCallContext context)
        {
            if (request.LotteryTypeID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "LotteryTypeID is required"));

            try
            {
                LotteriesReply reply = new();
                List<Models.Lottery> result = await _lotteryComponent.GetLotteriesByLotteryType(request.LotteryTypeID);

                if (result != null)
                    reply.Lotteries.AddRange(_mapper.Map<List<Models.Lottery>, List<LotteryModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLotteriesByLotteryType({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteriesByLotteryType() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteryModel> GetLottery(LotteryIDRequest request, ServerCallContext context)
        {
            if (request.LotteryID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "LotteryID is required"));

            try
            {
                LotteryModel reply = new();
                var result = await _lotteryComponent.GetLottery(request.LotteryID);

                if (result != null)
                    reply = _mapper.Map<Models.Lottery, LotteryModel>(result);
                else
                    context.Status = new Status(StatusCode.NotFound, "Lottery not found");

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLottery({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLottery() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteryCardsReply> GetLotteryCardsByLotteryID(LotteryIDRequest request, ServerCallContext context)
        {
            if (request.LotteryID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "LotteryID is required"));


            try
            {
                LotteryCardsReply reply = new();
                List<Models.LotteryCard> result = await _lotteryComponent.GetLotteryCardsByLotteryID(request.LotteryID);

                if (result != null)
                    reply.LotteryCards.AddRange(_mapper.Map<List<Models.LotteryCard>, List<LotteryCardModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLotteryCardsByLotteryID({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteryCardsByLotteryID() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteryTypesReply> GetLotteryTypes(EmptyRequest request, ServerCallContext context)
        {

            try
            {
                LotteryTypesReply reply = new();
                List<Models.LotteryType> result = await _lotteryComponent.GetLotteryTypes();

                if (result != null)
                    reply.LotteryTypes.AddRange(_mapper.Map<List<Models.LotteryType>, List<LotteryTypeModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLotteriesByLotteryType({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteriesByLotteryType() - Message: {ex.Message}"));
            }
        }


        #endregion

        #region Card

        public async override Task<CardModel> GetCard(GetCardRequest request, ServerCallContext context)
        {
            RequestValidators.ValidateGetCardRequest(request);

            try
            {
                Models.Card? card = await _cardComponent.GetCard(request.CardID);

                var reply = new CardModel();

                if (card == null)
                    context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
                else
                    reply = _mapper.Map<Models.Card, Definition.CardModel>(card);

                return reply;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCard({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetCard() - Message: {ex.Message}"));
            }
        }

        public async override Task<CardsReply> GetCards(GetCardsRequest request, ServerCallContext context)
        {
            RequestValidators.ValidateGetCardsRequest(request);

            try
            {
                List<Models.Card> cards = await _cardComponent.GetCards([.. request.CardIDs]);

                var reply = new CardsReply();

                if (cards == null)
                    context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
                else
                    reply.Cards.AddRange(_mapper.Map<List<Models.Card>, List<Definition.CardModel>>(cards));

                return reply;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCards({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetCards() - Message: {ex.Message}"));
            }
        }



        #endregion

        #region Board

        public async override Task<BoardModel> SaveBoard(SaveBoardRequest request, ServerCallContext context)
        {
            RequestValidators.ValidateSaveBoardRequest(request);

            try
            {
                Models.Board? board = _mapper.Map<SaveBoardRequest, Models.Board>(request);

                board = await _boardComponent.SaveBoard(board);
                var reply = new BoardModel();

                if (board == null)
                    context.Status = new Status(StatusCode.NotFound, "NotFound");
                else
                    reply = _mapper.Map<Models.Board, Definition.BoardModel>(board);

                return reply;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveBoard({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error SaveBoard() - Message: {ex.Message}"));
            }
        }

        public async override Task<BoardSizesReply> GetBoardSizes(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                List<Models.BoardSize> boardSizes = await _boardComponent.GetBoardSizes();
                var reply = new BoardSizesReply();

                if (boardSizes == null || boardSizes.Count == 0)
                    context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
                else
                    reply.BoardSizes.AddRange(_mapper.Map<List<Models.BoardSize>, List<Definition.BoardSizeModel>>(boardSizes));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBoardSizes()");
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetBoardSizes() - Message: {ex.Message}"));
            }
        }

        public async override Task<SuccessReply> DeleteBoard(DeleteBoardRequest request, ServerCallContext context)
        {
            RequestValidators.ValidateDeleteBoardRequest(request);

            try
            {
                bool success = await _boardComponent.DeleteBoard(request.BoardID);

                if (!success)
                    context.Status = new Status(StatusCode.NotFound, "NOT FOUND");

                return new SuccessReply { Success = success };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteBoard({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error DeleteBoard() - Message: {ex.Message}"));
            }
        }

        public async override Task<GetBoardsByUserIDReply> GetBoardsByUserID(UserIDRequest request, ServerCallContext context)
        {
            RequestValidators.ValidateGetBoardsByUserIDRequest(request);

            try
            {
                List<Models.Board> boards = await _boardComponent.GetBoardsByUserID(request.UserID);
                var reply = new GetBoardsByUserIDReply();

                if (boards == null || boards.Count == 0)
                    context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
                else
                    reply.Boards.AddRange(_mapper.Map<List<Models.Board>, List<Definition.BoardModel>>(boards));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBoardsByUserID({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetBoardsByUserID() - Message: {ex.Message}"));
            }
        }

        public async override Task<BoardModel> GetFullBoardByBoardID(BoardIDRequest request, ServerCallContext context)
        {
            RequestValidators.ValidateGetFullBoardByBoardIDRequest(request);

            try
            {
                Models.Board? board = await _boardComponent.GetFullBoardByBoardID(request.BoardID);
                var reply = new BoardModel();

                if (board == null)
                    context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
                else
                    reply = _mapper.Map<Models.Board, Definition.BoardModel>(board);

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFullBoardByBoardID({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetFullBoardByBoardID() - Message: {ex.Message}"));
            }
        }

        public async override Task<SuccessReply> SaveBoardCards(SaveBoardCardsRequest request, ServerCallContext context)
        {
            RequestValidators.ValidateSaveBoardCardsRequest(request);

            try
            {
                List<Models.BoardCard> boardCards = _mapper.Map<List<Definition.BoardCardModel>, List<Models.BoardCard>>([.. request.BoardCards]);

                bool success = await _boardComponent.SaveBoardCards(request.BoardID, boardCards);

                if (!success)
                    context.Status = new Status(StatusCode.NotFound, "NOT FOUND");

                return new SuccessReply { Success = success };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveBoardCards({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error SaveBoardCards() - Message: {ex.Message}"));
            }
        }



        public async override Task GetBoardDocumentStream(BoardDocumentRequest request, IServerStreamWriter<Chunk> responseStream, ServerCallContext context)
        {
            RequestValidators.ValidateGetBoardDocumentStreamRequest(request);

            try
            {
                await GetBoardDocumentStreamInternal(request.BoardID, responseStream);
            }
            catch (KeyNotFoundException)
            {
                context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBoardDocumentStream({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetBoardDocumentStream() - Message: {ex.Message}"));
            }
        }

        public async override Task GetBoardDocumentsStream(BoardDocumentsRequest request, IServerStreamWriter<Chunk> responseStream, ServerCallContext context)
        {
            RequestValidators.ValidateGetBoardDocumentsStreamRequest(request);

            try
            {
                await GetBoardDocumentsStreamInternal([.. request.BoardIDs], responseStream);
            }
            catch (KeyNotFoundException)
            {
                context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBoardDocumentsStream({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetBoardDocumentsStream() - Message: {ex.Message}"));
            }
        }

        public async override Task GetUserBoardsLiteDocumentStream(UserBoardsLiteDocumentRequest request, IServerStreamWriter<Chunk> responseStream, ServerCallContext context)
        {
            RequestValidators.ValidateUserBoardsLiteDocumentRequest(request);

            try
            {
                await GetUserBoardsLiteDocumentStreamInternal([.. request.UserBoards], responseStream);
            }
            catch (KeyNotFoundException)
            {
                context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserBoardsLiteDocumentStream({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetUserBoardsLiteDocumentStream() - Message: {ex.Message}"));
            }
        }

        internal async Task GetBoardDocumentStreamInternal(int boardId, IServerStreamWriter<Chunk> responseStream)
        {
            byte[] document = await _boardComponent.GetBoardDocument(boardId);

            await DocumentStreamInteral(document, responseStream);
        }

        internal async Task GetBoardDocumentsStreamInternal(List<int> boardIds, IServerStreamWriter<Chunk> responseStream)
        {
            byte[] document = await _boardComponent.GetBoardsDocument(boardIds);

            await DocumentStreamInteral(document, responseStream);
        }

        internal async Task GetUserBoardsLiteDocumentStreamInternal(List<BoardLiteModel> boards, IServerStreamWriter<Chunk> responseStream)
        {
            var userBoards = _mapper.Map<List<BoardLiteModel>, List<BoardLite>>(boards);
            byte[] document = await _boardComponent.GetUserBoardsLiteDocument(userBoards);

            await DocumentStreamInteral(document, responseStream);
        }

        internal async Task DocumentStreamInteral(byte[] document, IServerStreamWriter<Chunk> responseStream)
        {
            if (document == null || document.Length == 0)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                int chunkSize = int.Parse(_appConfiguration.GetValue("FILE_STREAM_CHUNK_SIZE"));
                decimal chunkCount = Math.Ceiling(document.Length / (decimal)chunkSize);
                int offset = 0;

                for (int i = 0; i < chunkCount; i++)
                {
                    int bytesToTake = Math.Min(chunkSize, document.Length - i * chunkSize);
                    byte[] chunk = document.Skip(offset).Take(bytesToTake).ToArray();

                    await responseStream.WriteAsync(new Chunk
                    {
                        Data = ByteString.CopyFrom(chunk)
                    });

                    offset += (chunk.Length);
                }
            }
        }

        #endregion

    }
}
