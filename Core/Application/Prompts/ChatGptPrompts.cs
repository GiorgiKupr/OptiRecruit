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
        private const string OptimizePrompt = @"
{
  ""instructions"": ""You are a precision-focused resume optimization engine. Analyze every requirement in the job description and cross-reference with the candidate's CV using these steps: 1. Extract ALL technical/functional requirements from job description 2. Map each requirement to: a) Direct CV matches b) Indirect/related CV experience c) Gaps (requirements with no CV basis) 3. For indirect matches: Reword CV content using job description's exact terminology while maintaining factual accuracy 4. Structure response to: - Mirror job description's priority order - Surface implicit connections through JD keywords - Maintain original CV information integrity"",
  ""response_format"": {
    ""Name"": ""Candidate's Name"",
    ""Email"": ""Candidate's Email"",
    ""Phone"": ""Candidate's Phone Number"",
    ""Summary"": ""Reconstructed profile containing: [Job Title] expertise with [Top 3 Job Priorities] experience, emphasizing [Job's Core Requirements] through [CV Experience]"",
    ""WorkExperiences"": [
      {
        ""Position"": ""Job Title (JD-Aligned)"",
        ""Company"": ""Company"",
        ""Duration"": ""Dates"",
        ""Description"": [
          ""JD Requirement 1: [CV Experience Connection]"",
          ""JD Requirement 2: [Relevant CV Achievement]"",
          ""[Job Description Phrasing] via [CV Implementation Details]""
        ]
      }
    ],
    ""Skills"": [
      ""[Exact Job Description Term 1]"",
      ""[Exact Job Description Term 2]"",
      ""[CV Skill Supporting JD Requirement]"",
      ""[Inferred JD Term from CV Context]""
    ]
  },
  ""processing_rules"": [
    ""Never add completely missing technologies"",
    ""Convert CV language to job description equivalents: 'API development' → 'RESTful microservices' if JD requires"",
    ""Prioritize skills section with JD's most frequent requirements"",
    ""In work experience bullets: Start with JD keyword, then supporting CV detail"",
    ""Treat architecture/pattern requirements as implicit in related CV tech stack""
  ]
}
";
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
