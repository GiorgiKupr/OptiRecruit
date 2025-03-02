using Application.Abstraction;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace OptiRecruit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeTailorerService _resumeTailor;

        public ResumeController(IResumeTailorerService resumeTailor)
        {
            _resumeTailor = resumeTailor;   
        }

        [HttpPost("tailor-job-and-cv")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ParseResume([FromForm] ResumeTailorRequest obj)
        {
            using var stream = obj.File.OpenReadStream();
            var TailoredResumeByteArray = await _resumeTailor.ResumeBuilder(stream, obj.JobDesc);
            return File(TailoredResumeByteArray, "application/pdf", "Resume.pdf");
        }
    }
}
