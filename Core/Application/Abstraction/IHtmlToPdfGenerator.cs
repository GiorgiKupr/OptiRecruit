namespace Application.Abstraction
{
    public interface IHtmlToPdfGenerator
    {
        byte[] GeneratePdf(string html);
    }
}
