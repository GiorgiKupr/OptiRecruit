using Newtonsoft.Json;
namespace Infrastructure.Services.Models.ChatGPT
{
    public class ChatGptApiResponse
    {
        [JsonProperty("choices")]
        public List<ChatGptChoice> Choices { get; set; }
    }
}
