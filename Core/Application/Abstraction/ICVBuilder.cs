using Domain.Models.Resume;

public interface ICVBuilder
{
    byte[] GenerateResumePdf(Resume resume);
}