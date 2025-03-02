using Newtonsoft.Json;

namespace Domain.Models.ChatGPT
{
    /// <summary>
    /// Role = User Or Chatgpt
    /// Content = Request, Response.
    /// </summary>
    public class ChatMessage
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
