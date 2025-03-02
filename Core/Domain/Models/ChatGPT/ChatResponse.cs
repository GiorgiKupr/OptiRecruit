namespace Domain.Models.ChatGPT
{
    /// <summary>
    /// Response that Chatgpt returns,
    /// it contains object which is ChatMessage that includes "Role" which is "User" or "ChatGPT" itself
    /// and Content which is request Or Response.
    /// </summary>
    public class ChatResponse
    {
        public ChatMessage Message { get; set; }
    }
}
