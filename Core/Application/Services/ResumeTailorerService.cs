using Application.Abstraction;

namespace Application.Services
{
    public class ResumeTailorerService : IResumeTailorerService
    {
        private readonly IResumeOptimizerService _resumeOptimizer;
        private readonly IResumeParserService _resumeParser;
        private readonly ICVBuilder _CVBuilder;

        public ResumeTailorerService(IResumeOptimizerService resumeOptimizer, IResumeParserService resumeParser, ICVBuilder CVBuilder)
        {
            _resumeOptimizer = resumeOptimizer;
            _resumeParser = resumeParser;
            _CVBuilder = CVBuilder;
        }

        public async Task<byte[]> ResumeBuilder(Stream resume, string JobDesription)
        {
            var parsedResumeObject = await _resumeParser.ParseResume(resume);
            var optimizedResumeObject = await _resumeOptimizer.OptimizeResumeForJob(new OptimizeResumeDto(parsedResumeObject, JobDesription));
            var tailoredResumeByteArray = _CVBuilder.GenerateResumePdf(optimizedResumeObject);
            return tailoredResumeByteArray;
        }
    }
}
