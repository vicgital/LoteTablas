using MediatR;

namespace LoteTablas.Grpc.Document.Application.Features.PdfGenerator
{
    public record PdfGeneratorRequest(string BodyHtml, string HeaderHtml, string FooterHtml) : IRequest<byte[]>;
}
