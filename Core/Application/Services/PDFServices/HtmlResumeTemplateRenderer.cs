using Domain.Models.Resume;
using System.Net;
using System.Text;
using Application.Abstraction;

public class HtmlResumeTemplateRenderer : IHtmlResumeTemplateRenderer
{
    private readonly string _templatePath;

    public HtmlResumeTemplateRenderer(string templatePath)
    {
        _templatePath = templatePath;
    }

    public string RenderResume(Resume resume)
    {
        string html = File.ReadAllText(_templatePath);
        string contactLine = string.Join(" | ",
            new[] { resume.Email, resume.Phone }
            .Where(s => !string.IsNullOrEmpty(s))
        );
        html = html.Replace("{{Name}}", WebUtility.HtmlEncode(resume.Name ?? ""));
        html = html.Replace("{{ContactLine}}", WebUtility.HtmlEncode(contactLine));
        html = html.Replace("{{Summary}}", WebUtility.HtmlEncode(resume.Summary ?? ""));

        bool hasSummary = !string.IsNullOrEmpty(resume.Summary);
        if (hasSummary)
        {
            html = html.Replace("{{#HasSummary}}", "")
                       .Replace("{{/HasSummary}}", "");
        }
        else
        {
            html = RemoveBlock(html, "{{#HasSummary}}", "{{/HasSummary}}");
        }
        bool hasExperience = (resume.WorkExperiences != null && resume.WorkExperiences.Any());
        if (hasExperience)
        {
            html = html.Replace("{{#HasExperience}}", "")
                       .Replace("{{/HasExperience}}", "");
            var sbExp = new StringBuilder();
            foreach (var exp in resume.WorkExperiences)
            {
                sbExp.Append("<div class=\"experience-item\">");
                sbExp.Append("<div class=\"experience-header\">");
                sbExp.Append($"<div class=\"experience-position\">{WebUtility.HtmlEncode(exp.Position ?? "")}</div>");
                sbExp.Append($"<div class=\"experience-duration\">{WebUtility.HtmlEncode(exp.Duration ?? "")}</div>");
                sbExp.Append("</div>");
                sbExp.Append($"<div class=\"experience-company\">{WebUtility.HtmlEncode(exp.Company ?? "")}</div>");

                sbExp.Append("<ul class=\"experience-description\">");
                if (!string.IsNullOrEmpty(exp.Description))
                {
                    var lines = exp.Description.Split('\n');
                    foreach (var line in lines)
                    {
                        var trimmed = line.Trim();
                        if (!string.IsNullOrEmpty(trimmed))
                        {
                            sbExp.Append("<li>");
                            sbExp.Append(WebUtility.HtmlEncode(trimmed));
                            sbExp.Append("</li>");
                        }
                    }
                }
                sbExp.Append("</ul>");
                sbExp.Append("</div>"); 
            }
            html = InsertBlock(html, "{{#WorkExperiences}}", "{{/WorkExperiences}}", sbExp.ToString());
        }
        else
        {
            html = RemoveBlock(html, "{{#HasExperience}}", "{{/HasExperience}}");
        }

        bool hasSkills = (resume.Skills != null && resume.Skills.Count > 0);
        if (hasSkills)
        {
            html = html.Replace("{{#HasSkills}}", "")
                       .Replace("{{/HasSkills}}", "");

            var sbSkills = new StringBuilder();
            foreach (var skill in resume.Skills)
            {
                sbSkills.Append($"<li>{WebUtility.HtmlEncode(skill)}</li>");
            }
            html = InsertBlock(html, "{{#Skills}}", "{{/Skills}}", sbSkills.ToString());
        }
        else
        {
            html = RemoveBlock(html, "{{#HasSkills}}", "{{/HasSkills}}");
        }

        return html;
    }

    private string RemoveBlock(string text, string startToken, string endToken)
    {
        int startIdx = text.IndexOf(startToken);
        if (startIdx == -1) return text;
        int endIdx = text.IndexOf(endToken, startIdx);
        if (endIdx == -1) return text;
        endIdx += endToken.Length;
        return text.Remove(startIdx, endIdx - startIdx);
    }
    private string InsertBlock(string text, string startToken, string endToken, string snippet)
    {
        int startIdx = text.IndexOf(startToken);
        if (startIdx == -1) return text;
        int endIdx = text.IndexOf(endToken, startIdx);
        if (endIdx == -1) return text;
        endIdx += endToken.Length;
        string before = text.Substring(0, startIdx);
        string after = text.Substring(endIdx);
        return before + snippet + after;
    }
}


