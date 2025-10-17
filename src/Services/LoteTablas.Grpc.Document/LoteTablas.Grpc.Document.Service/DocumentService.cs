using Google.Protobuf;
using Grpc.Core;
using LoteTablas.Application.Contracts.Configuration;
using LoteTablas.Grpc.Definitions;
using LoteTablas.Grpc.Definitions.Common;
using LoteTablas.Grpc.Document.Application.Features.PdfGenerator;
using MediatR;
using static LoteTablas.Grpc.Definitions.Document;

namespace LoteTablas.Grpc.Document.Service
{
    public class DocumentService(ILogger<DocumentService> logger,
        IAppConfigurationManager appConfigurationManager,
        IMediator mediator) : DocumentBase
    {
        private readonly ILogger<DocumentService> _logger = logger;
        private readonly IAppConfigurationManager _appConfigurationManager = appConfigurationManager;
        private readonly IMediator _mediator = mediator;

        public async override Task ConvertHtmlToPDF(ConvertHtmlToPDFRequest request, IServerStreamWriter<DataChunk> responseStream, ServerCallContext context)
        {

            if (string.IsNullOrEmpty(request.BodyHtml))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "BodyHtml is required"));

            try
            {
                var document = await _mediator.Send(new PdfGeneratorRequest(request.BodyHtml, request.HeaderHtml, request.FooterHtml));
                await DocumentStreamInteral(document, responseStream);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error - ConvertHtmlToPDF()");
                throw new RpcException(new Status(StatusCode.Internal, $"Error ConvertHtmlToPDF() - Message: {ex.Message}"));
            }

        }

        internal async Task DocumentStreamInteral(byte[] document, IServerStreamWriter<DataChunk> responseStream)
        {
            if (document == null || document.Length == 0)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                int chunkSize = int.Parse(_appConfigurationManager.GetValue("FILE_STREAM_CHUNK_SIZE"));
                decimal chunkCount = Math.Ceiling(document.Length / (decimal)chunkSize);
                int offset = 0;

                for (int i = 0; i < chunkCount; i++)
                {
                    int bytesToTake = Math.Min(chunkSize, document.Length - i * chunkSize);
                    byte[] chunk = [.. document.Skip(offset).Take(bytesToTake)];

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
