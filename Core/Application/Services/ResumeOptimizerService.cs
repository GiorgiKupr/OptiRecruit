using Application.Abstraction;
using Application.Prompts;
using Domain.Models.ChatGPT;
using Domain.Models.Resume;
using Newtonsoft.Json;

namespace Application.Services
{
    public class ResumeOptimizerService : IResumeOptimizerService
    {
        private readonly IChatClient _conversationClient;
        public ResumeOptimizerService(IChatClient chatClient)
        {
            _conversationClient = chatClient;
        }
        public async Task<Resume> OptimizeResumeForJob(OptimizeResumeDto model)
        {
            string resumeJson = JsonConvert.SerializeObject(model.parsedResume);
            string prompt = ChatGptPrompts.CreateOptimizationPrompt(resumeJson, model.jobDescription);
            var messages = new List<ChatMessage> { new ChatMessage { Role = "user", Content = prompt } };
            var chatCompletion = await _conversationClient.GetResponseAsync(messages);
            string jsonResponse = chatCompletion.Message.Content.Trim();
            Resume tailoredResume = JsonConvert.DeserializeObject<Resume>(jsonResponse);
            return tailoredResume;
        }
    }
}
