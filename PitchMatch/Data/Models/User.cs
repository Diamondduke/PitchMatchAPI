using System.ComponentModel.DataAnnotations;

namespace PitchMatch.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A name is required.")]
        [MinLength(3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        [MaxLength(100, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "A unique email is required.")]
        [MinLength(3, ErrorMessage = "Email must be between 3 and 100 characters.")]
        [MaxLength(100, ErrorMessage = "Email must be between 3 and 100 characters.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least six characters.")]
        [MaxLength(300, ErrorMessage = "We don't have space for that much password!")]
        public string Password { get; set; } = string.Empty;
        [MaxLength(2000, ErrorMessage = "A biography can be 2000 characters at most. A short and snappy introduction works best!")]
        public string? Bio { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? Contact { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? SoMe { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? ImgUrl { get; set; }
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 300 character limit.")]
        public string? CvUrl { get; set; }
        public string Salt { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Rating { get; set; }
        public PersonalData? PersonalData { get; set; }
        public ICollection<Pitch>? Pitches { get; set; }
        public ICollection<Investment>? Investments { get; set; }
    }
}
