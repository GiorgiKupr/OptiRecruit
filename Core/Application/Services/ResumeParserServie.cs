using Application.Abstraction;
using Application.Prompts;
using Domain.Models.ChatGPT;
using Domain.Models.Resume;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class ResumeParserServie : IResumeParserService
    {
        private readonly IChatClient _conversationClient;
        public ResumeParserServie(IChatClient chatClient)
        {
            _conversationClient = chatClient;
        }
        
        /// <summary>
        /// Receive stream of Cv/Resume file
        /// Returns parsed resume object.
        /// </summary>
        /// <param name="resumeStream"></param>
        /// <returns>Parsed Resume object</returns>
        public async Task<Resume> ParseResume(Stream resumeStream)
        {
            string resumeText = ExtractTextFromPdf(resumeStream);
            string prompt = ChatGptPrompts.CreateParsePrompt(resumeText);
            var messages = new List<ChatMessage> { new ChatMessage { Role = "user", Content = prompt } };
            var chatCompletion = await _conversationClient.GetResponseAsync(messages);
            Console.WriteLine(chatCompletion.Message.Content);
            Resume parsedResume = null;
            try
            {
                string jsonResponse = chatCompletion.Message.Content.Trim();
                parsedResume = JsonSerializer.Deserialize<Resume>(jsonResponse);

            }
            catch (Exception ex)
            {
                throw new Exception("დესერიალიზაციის დროს" + ex.ToString());
            }
            return parsedResume;
        }

        /// <summary>
        /// Extracts text from cv, line by line.
        /// </summary>
        /// <param name="pdfStream"></param>
        /// <returns></returns>
        private string ExtractTextFromPdf(Stream pdfStream)
        {
            using var reader = new PdfReader(pdfStream);
            var sb = new StringBuilder();
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                sb.AppendLine(PdfTextExtractor.GetTextFromPage(reader, i));
            }
            return sb.ToString();
        }
    }
}
