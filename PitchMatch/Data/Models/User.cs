using System.ComponentModel.DataAnnotations;

namespace PitchMatch.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string? Bio {  get; set; }
        public string? Contact { get; set; }
        public string? SoMe { get; set; }
        public string? ImgUrl { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Rating { get; set; }
        public PersonalData? PersonalData { get; set; }
        public ICollection<Pitch>? Pitches { get; set; }
        public ICollection<Investment>? Investments { get; set; }
    }
}
