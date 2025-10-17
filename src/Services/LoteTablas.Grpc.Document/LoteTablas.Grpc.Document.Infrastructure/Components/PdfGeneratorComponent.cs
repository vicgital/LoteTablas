using LoteTablas.Application.Contracts.Configuration;
using LoteTablas.Grpc.Document.Application.Contracts;
using NReco.PdfGenerator;
using System.Runtime.InteropServices;

namespace LoteTablas.Grpc.Document.Infrastructure.Components
{
    public class PdfGeneratorComponent(
        IAppConfigurationManager appConfigurationManager) : IPdfGeneratorComponent
    {
        private readonly IAppConfigurationManager _appConfiguration = appConfigurationManager;
        private readonly OSPlatform _OSPlatform = GetCurrentOS();


        public byte[] ConvertHtmlToPDF(string bodyHtml, string headerHtml = "", string footerHtml = "")
        {
            var converter = InitializePDFConverter();

            if (!string.IsNullOrEmpty(headerHtml))
                converter.PageHeaderHtml = headerHtml;

            if (!string.IsNullOrEmpty(footerHtml))
                converter.PageFooterHtml = footerHtml;

            converter.Size = NReco.PdfGenerator.PageSize.Letter;
            converter.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;

            byte[] file = converter.GeneratePdf(bodyHtml);
            return file;

        }

        private HtmlToPdfConverter InitializePDFConverter()
        {

            var converter = new NReco.PdfGenerator.HtmlToPdfConverter();

            string wkHtmlToPdfLicenseOwner = _appConfiguration.GetValue("WKHTML_TO_PDF_LICENSE_OWNER");
            string wkHtmlToPdfLicenseKey = _appConfiguration.GetValue("WKHTML_TO_PDF_LICENSE_KEY");
            string wkHtmlToPdfExeNameWindows = _appConfiguration.GetValue("WKHTML_TO_PDF_EXE_NAME_WINDOWS", "wkhtmltopdf.exe");
            string wkHtmlToPdfExeNameLinux = _appConfiguration.GetValue("WKHTML_TO_PDF_EXE_NAME_LINUX", "wkhtmltopdf");

            string licenseOwner = wkHtmlToPdfLicenseOwner;
            string licenseKey = wkHtmlToPdfLicenseKey;
            converter.License.SetLicenseKey(licenseOwner, licenseKey);
            string? rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (_OSPlatform == OSPlatform.Windows)
            {
                converter.WkHtmlToPdfExeName = wkHtmlToPdfExeNameWindows;
                converter.PdfToolPath = rootFolder + "\\wkhtmltopdf\\windows";
            }
            else if (_OSPlatform == OSPlatform.Linux)
            {
                converter.WkHtmlToPdfExeName = wkHtmlToPdfExeNameLinux;
                converter.PdfToolPath = rootFolder + "/wkhtmltopdf/linux";
            }
            else if (_OSPlatform == OSPlatform.OSX)
            {
                converter.WkHtmlToPdfExeName = wkHtmlToPdfExeNameLinux;
                converter.PdfToolPath = rootFolder + "/wkhtmltopdf/osx";
            }

            return converter;
        }

        private static OSPlatform GetCurrentOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return OSPlatform.Windows;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return OSPlatform.Linux;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return OSPlatform.OSX;

            return OSPlatform.Windows;

        }

    }
}
