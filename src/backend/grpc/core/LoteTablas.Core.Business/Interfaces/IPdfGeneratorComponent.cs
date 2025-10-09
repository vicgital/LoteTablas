namespace LoteTablas.Core.Business.Interfaces
{
    public interface IPdfGeneratorComponent
    {
        byte[] ConvertHtmlToPDF(string bodyHtml, string headerHtml = "", string footerHtml = "");
    }
}
