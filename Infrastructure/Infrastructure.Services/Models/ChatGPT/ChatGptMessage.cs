using Newtonsoft.Json;
namespace Infrastructure.Services.Models.ChatGPT
{
    public class ChatGptMessage
    {
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
