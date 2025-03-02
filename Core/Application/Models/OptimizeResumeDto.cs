using Domain.Models.Resume;

namespace Application.Services
{
    public record OptimizeResumeDto(Resume parsedResume, string jobDescription);
}
