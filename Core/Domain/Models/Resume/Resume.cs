using System.Text.Json.Serialization;

namespace Domain.Models.Resume
{
    public class Resume
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("Phone")]
        public string Phone { get; set; }

        [JsonPropertyName("Summary")]
        public string Summary { get; set; }

        [JsonPropertyName("WorkExperiences")]
        public List<WorkExperience> WorkExperiences { get; set; }

        [JsonPropertyName("Skills")]
        public List<string> Skills { get; set; }
    }
}
