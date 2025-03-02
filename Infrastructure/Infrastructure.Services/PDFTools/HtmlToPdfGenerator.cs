using Application.Abstraction;
using NReco.PdfGenerator;

namespace Infrastructure.Services.PDFTools
{
    public class HtmlToPdfGenerator : IHtmlToPdfGenerator
    {
        public byte[] GeneratePdf(string html)
        {
            var converter = new HtmlToPdfConverter
            {
                Zoom = 1.0f,
                Size = PageSize.A4
            };
            return converter.GeneratePdf(html);
        }
    }
}