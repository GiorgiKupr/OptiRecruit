using Application.Services;
using Domain.Models.Resume;

namespace Application.Abstraction
{
    public interface IResumeOptimizerService
    {
        Task<Resume> OptimizeResumeForJob(OptimizeResumeDto model);
    }
}