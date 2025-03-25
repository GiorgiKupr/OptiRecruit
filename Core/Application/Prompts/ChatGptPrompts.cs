using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Prompts
{
    public static class ChatGptPrompts
    {
        private const string attempt = "Please return only valid JSON, with no code fences or markdown formatting.";
        private const string ParsePrompt = "You are an expert resume parser. Given the resume text below, extract the following details and return a JSON object with these fields: Name, Email, Phone, Summary, WorkExperiences (each with Position, Company, Duration, Description), and Skills (as a list). Return only valid JSON without any extra commentary.\n\nResume Text:\n";
        private const string OptimizePrompt = "\"You are an expert resume tailor and career optimization specialist. \"\r\n    \"Your task is to refine and tailor the given resume so that it aligns perfectly with the provided job description. \"\r\n    \"Ensure the following:\\n\"\r\n    \"- Emphasize skills, experiences, and achievements that match the job description.\\n\"\r\n    \"- If a required skill (e.g., Python) is missing in the parsed resume but is relevant to the candidate's background, intelligently infer and include it.\\n\"\r\n    \"- Reword bullet points and descriptions to highlight the most relevant aspects of the candidate’s experience.\\n\"\r\n    \"- Maintain authenticity—avoid fabricating experience, but reframe existing work to showcase alignment with the job.\\n\"\r\n    \"- If needed, reorder sections to highlight the most crucial details first.\\n\"\r\n    \"- Use strong, industry-relevant action verbs to enhance impact.\\n\\n\"\r\n    \"Return a JSON object with the following structure:\\n\"\r\n    \"{\\n\"\r\n    '  \"Name\": \"Candidate\\'s Name\",\\n'\r\n    '  \"Email\": \"Candidate\\'s Email\",\\n'\r\n    '  \"Phone\": \"Candidate\\'s Phone Number\",\\n'\r\n    '  \"Summary\": \"A concise and compelling summary aligning with the job description.\",\\n'\r\n    '  \"WorkExperiences\": [\\n'\r\n    \"    {\\n\"\r\n    '      \"Position\": \"Job Title\",\\n'\r\n    '      \"Company\": \"Company Name\",\\n'\r\n    '      \"Duration\": \"Time Period\",\\n'\r\n    '      \"Description\": \"Refined, optimized bullet points emphasizing achievements and responsibilities most relevant to the job.\"\\n'\r\n    \"    }\\n\"\r\n    \"  ],\\n\"\r\n    '  \"Skills\": [\"List of relevant skills, ensuring all key job description requirements are included\"]\\n'\r\n    \"}\\n\\n\"\r\n    \"Job Description:\\n\"\r\n    '\" + jobDescription + \"\\n\\n'\r\n    \"Parsed Resume JSON:\\n\"\r\n    '\" + parsedResumeJson + \"'";
        public static string CreateParsePrompt(string resumeText)
        {
            return attempt + ParsePrompt + resumeText;
        }
        public static string CreateOptimizationPrompt(string resumeJson, string jobDescription)
        {
            return attempt + OptimizePrompt + resumeJson;
        }

    }
}
