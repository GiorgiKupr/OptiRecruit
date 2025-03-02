using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ResumeTailorRequest
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string JobDesc { get; set; }
    }
}
