using AutoMapper;
using Grpc.Core;
using LoteTablas.Grpc.Definitions;
using LoteTablas.Grpc.Definitions.Common;
using MediatR;

namespace LoteTablas.Grpc.Board.Service
{
    public class BoardService(
        ILogger<BoardService> logger,
        IMediator mediator,
        IMapper mapper
        ) : Definitions.Board.BoardBase
    {
        private readonly ILogger<BoardService> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;


        public override Task GetBoardDocumentStream(BoardDocumentRequest request, IServerStreamWriter<DataChunk> responseStream, ServerCallContext context)
        {
            return base.GetBoardDocumentStream(request, responseStream, context);
        }

        public override Task GetBoardDocumentsStream(BoardDocumentsRequest request, IServerStreamWriter<DataChunk> responseStream, ServerCallContext context)
        {
            return base.GetBoardDocumentsStream(request, responseStream, context);
        }
        
        
        
    }
}
