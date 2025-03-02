using Application.Abstraction;
using Infrastructure.Services.PDFTools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            var apiKey = configuration.GetValue<string>("OpenAI:ApiKey");
            var model = configuration.GetValue<string>("OpenAI:Model", "gpt-4");
            var store = configuration.GetValue<bool>("OpenAI:Store", false);


            services.AddHttpClient("OpenAI", client =>
            {
                client.BaseAddress = new Uri("https://api.openai.com/v1/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            });

            services.AddSingleton<IChatClient>(sp =>
            {
                var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var openAiClient = clientFactory.CreateClient("OpenAI");

                return new ChatGptClient(openAiClient, model, store);
            });

            services.AddSingleton<IHtmlToPdfGenerator, HtmlToPdfGenerator>();
            return services;
        }
    }
}
