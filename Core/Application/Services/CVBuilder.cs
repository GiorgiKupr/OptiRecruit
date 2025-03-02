using Domain.Models.Resume;
using Application.Abstraction;

public class CVBuilder : ICVBuilder
{
    private readonly IHtmlResumeTemplateRenderer _templateRenderer;
    private readonly IHtmlToPdfGenerator _pdfGenerator;

    public CVBuilder(
        IHtmlResumeTemplateRenderer templateRenderer,
        IHtmlToPdfGenerator pdfGenerator)
    {
        _templateRenderer = templateRenderer;
        _pdfGenerator = pdfGenerator;
    }

    public byte[] GenerateResumePdf(Resume resume)
    {
        var htmlContent = _templateRenderer.RenderResume(resume);
        var pdfBytes = _pdfGenerator.GeneratePdf(htmlContent);

        return pdfBytes;
    }
}


