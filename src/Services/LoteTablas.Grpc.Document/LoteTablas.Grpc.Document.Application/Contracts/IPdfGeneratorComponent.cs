namespace LoteTablas.Grpc.Document.Application.Contracts
{
    public interface IPdfGeneratorComponent
    {
        byte[] ConvertHtmlToPDF(string bodyHtml, string headerHtml = "", string footerHtml = "");
    }
}
