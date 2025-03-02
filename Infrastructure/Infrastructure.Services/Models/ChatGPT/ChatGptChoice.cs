using Newtonsoft.Json;
namespace Infrastructure.Services.Models.ChatGPT
{
    public class ChatGptChoice
    {
        [JsonProperty("message")]
        public ChatGptMessage Message { get; set; }
    }
}
