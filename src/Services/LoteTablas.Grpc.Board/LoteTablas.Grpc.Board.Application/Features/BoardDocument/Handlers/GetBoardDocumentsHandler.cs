using LoteTablas.Grpc.Board.Application.Contracts.Clients.Grpc;
using LoteTablas.Grpc.Board.Application.Contracts.Components;
using LoteTablas.Grpc.Board.Application.Features.BoardDocument.Requests;
using MediatR;

namespace LoteTablas.Grpc.Board.Application.Features.BoardDocument.Handlers
{
    public class GetBoardDocumentsHandler(
        ICardGrpcClient cardGrpcClient,
        IDocumentGrpcClient documentGrpcClient,
        IBoardBuilderComponent boardBuilderComponent
        ) : IRequestHandler<GetBoardDocumentsRequest, byte[]>
    {
        private readonly ICardGrpcClient _cardGrpcClient = cardGrpcClient;
        private readonly IDocumentGrpcClient _documentGrpcClient = documentGrpcClient;
        private readonly IBoardBuilderComponent _boardBuilderComponent = boardBuilderComponent;

        public async Task<byte[]> Handle(GetBoardDocumentsRequest request, CancellationToken cancellationToken)
        {

            List<BoardBuilder.DTO.DocumentBoardDto> documentBoardDtos = [];
            foreach (var boardDocument in request.BoardDocuments)
            {
                var cardIds = boardDocument.BoardCards.Select(e => e.CardId);

                // Get Cards from Card Service
                var cards = await _cardGrpcClient.GetCards([.. cardIds]);

                // Build Board Html
                var documentBoard = new BoardBuilder.DTO.DocumentBoardDto
                {
                    BoardCards = [.. (from boardCard in boardDocument.BoardCards
                                           join card in cards on boardCard.CardId equals card.CardId
                                           select new BoardBuilder.DTO.DocumentBoardCardDto
                                           {
                                               ImageUrl = card.ImagePath,
                                               Ordinal = boardCard.Ordinal
                                           })]
                };

                documentBoardDtos.Add(documentBoard);
            }



            var html = await _boardBuilderComponent.GetBoardsDocumentHtml(documentBoardDtos);

            // Generate Document
            var documentBytes = await _documentGrpcClient.ConvertHtmlToPDF(html);

            return documentBytes;
        }
    }
}
