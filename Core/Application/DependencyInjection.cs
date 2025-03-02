using Application.Abstraction;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            string applicationProjectRoot = Path.Combine(Directory.GetCurrentDirectory(), "..", "Application");
            string templatePath = "C:\\Users\\giorgi.kuprava\\source\\repos\\OptiRecruit\\Core\\Application\\Services\\PDFServices\\cv_template.html";

            services.AddSingleton<ICVBuilder,CVBuilder>();
            services.AddSingleton<IHtmlResumeTemplateRenderer>(provider => new HtmlResumeTemplateRenderer(templatePath));



            services.AddScoped<IResumeParserService, ResumeParserServie>();
            services.AddScoped<IResumeOptimizerService, ResumeOptimizerService>();
            services.AddScoped<IResumeTailorerService,ResumeTailorerService>();
            return services;
        }
    }
}
