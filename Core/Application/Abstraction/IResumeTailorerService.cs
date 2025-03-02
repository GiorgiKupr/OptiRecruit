namespace Application.Abstraction
{
    public interface IResumeTailorerService
    {
        Task<byte[]> ResumeBuilder(Stream resume, string JobDesription);
    }
}