using Application.Abstraction;
using Domain.Models.ChatGPT;
using Infrastructure.Services.Models.ChatGPT;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Services
{
    public class ChatGptClient : IChatClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _model;
        private readonly bool _store;

        public ChatGptClient(HttpClient httpClient, string model, bool store)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _model = model;
            _store = store;
        }

        public async Task<ChatResponse> GetResponseAsync(IList<ChatMessage> messages, CancellationToken cancellationToken = default)
        {
            var requestBody = new
            {
                model = _model,
                store = _store,
                messages = messages
            };

            var jsonRequest = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "chat/completions")
            {
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(requestMessage, cancellationToken);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"OpenAI API request failed: {ex.Message}");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(jsonResponse))
            {
                throw new Exception("OpenAI API returned an empty response.");
            }

            var apiResponse = JsonConvert.DeserializeObject<ChatGptApiResponse>(jsonResponse);
            if (apiResponse?.Choices == null || apiResponse.Choices.Count == 0 || string.IsNullOrWhiteSpace(apiResponse.Choices[0].Message?.Content))
            {
                throw new Exception("Invalid response from OpenAI API");
            }

            return new ChatResponse
            {
                Message = new ChatMessage
                {
                    Role = apiResponse.Choices[0].Message.Role,
                    Content = apiResponse.Choices[0].Message.Content.Trim()
                }
            };
        }
    }
}
