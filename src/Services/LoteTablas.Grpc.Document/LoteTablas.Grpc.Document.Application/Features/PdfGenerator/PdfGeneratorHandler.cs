using LoteTablas.Grpc.Document.Application.Contracts;
using MediatR;

namespace LoteTablas.Grpc.Document.Application.Features.PdfGenerator
{
    public class PdfGeneratorHandler(IPdfGeneratorComponent pdfGeneratorComponent) : IRequestHandler<PdfGeneratorRequest, byte[]>
    {
        private readonly IPdfGeneratorComponent _pdfGeneratorComponent = pdfGeneratorComponent;

        public async Task<byte[]> Handle(PdfGeneratorRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            var bytes = _pdfGeneratorComponent.ConvertHtmlToPDF(request.BodyHtml, request.HeaderHtml, request.FooterHtml);
            return bytes;
        }
    }
}
