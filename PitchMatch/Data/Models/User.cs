using System.ComponentModel.DataAnnotations;

namespace PitchMatch.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        [MaxLength(300)]
        public string Password { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string? Bio { get; set; }
        [MaxLength(300)]
        public string? Contact { get; set; }
        [MaxLength(300)]
        public string? SoMe { get; set; }
        [MaxLength(300)]
        public string? ImgUrl { get; set; }
        [MaxLength(300)]
        public string? CvUrl { get; set; }
        public string Salt { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Rating { get; set; }
        public PersonalData? PersonalData { get; set; }
        public ICollection<Pitch>? Pitches { get; set; }
        public ICollection<Investment>? Investments { get; set; }
    }
}
