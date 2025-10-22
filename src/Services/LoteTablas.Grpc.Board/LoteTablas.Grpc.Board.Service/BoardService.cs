using AutoMapper;
using Google.Protobuf;
using Grpc.Core;
using LoteTablas.Application.Contracts.Configuration;
using LoteTablas.Grpc.Board.Application.Features.BoardDocument.DTO;
using LoteTablas.Grpc.Board.Application.Features.BoardDocument.Requests;
using LoteTablas.Grpc.Board.Service.Helpers;
using LoteTablas.Grpc.Definitions;
using LoteTablas.Grpc.Definitions.Common;
using MediatR;

namespace LoteTablas.Grpc.Board.Service
{
    public class BoardService(
        ILogger<BoardService> logger,
        IMediator mediator,
        IMapper mapper,
        IAppConfigurationManager appConfiguration
        ) : Definitions.Board.BoardBase
    {
        private readonly ILogger<BoardService> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly IAppConfigurationManager _appConfiguration = appConfiguration;


        public override async Task GetBoardDocumentStream(BoardDocumentRequest request, IServerStreamWriter<DataChunk> responseStream, ServerCallContext context)
        {
            RequestValidator.ValidateBoardDocumentRequest(request);

            try
            {
                var dto = _mapper.Map<BoardDocumentModel, BoardDocumentRequestDto>(request.Board);
                var data = await _mediator.Send(new GetBoardDocumentRequest(dto));
                await DocumentStreamInteral(data, responseStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBoardDocumentStream({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetBoardDocumentStream() - Message: {ex.Message}"));
            }
        }

        public async override Task GetBoardDocumentsStream(BoardDocumentsRequest request, IServerStreamWriter<DataChunk> responseStream, ServerCallContext context)
        {
            RequestValidator.ValidateBoardDocumentsRequest(request);

            try
            {
                var dto = _mapper.Map<List<BoardDocumentModel>, List<BoardDocumentRequestDto>>([.. request.Boards]);
                var data = await _mediator.Send(new GetBoardDocumentsRequest(dto));
                await DocumentStreamInteral(data, responseStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBoardDocumentsStream({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetBoardDocumentsStream() - Message: {ex.Message}"));
            }
        }

        internal async Task DocumentStreamInteral(byte[] document, IServerStreamWriter<DataChunk> responseStream)
        {
            if (document == null || document.Length == 0)
                throw new KeyNotFoundException();
            else
            {
                int chunkSize = int.Parse(_appConfiguration.GetValue("FILE_STREAM_CHUNK_SIZE"));
                decimal chunkCount = Math.Ceiling(document.Length / (decimal)chunkSize);
                int offset = 0;

                for (int i = 0; i < chunkCount; i++)
                {
                    int bytesToTake = Math.Min(chunkSize, document.Length - i * chunkSize);
                    byte[] chunk = document.Skip(offset).Take(bytesToTake).ToArray();

                    await responseStream.WriteAsync(new DataChunk
                    {
                        Data = ByteString.CopyFrom(chunk)
                    });

                    offset += (chunk.Length);
                }
            }
        }



    }
}
