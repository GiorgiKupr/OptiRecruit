using Domain.Models.Resume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction
{
    public interface IHtmlResumeTemplateRenderer
    {
        string RenderResume(Resume resume);
    }

}
