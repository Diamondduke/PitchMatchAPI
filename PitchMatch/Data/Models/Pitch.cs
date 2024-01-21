using Microsoft.Identity.Client;
using PitchMatch.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace PitchMatch.Data.Models
{
    public class Pitch
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        [MinLength(3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        [MaxLength(100, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "A summary is required.")]
        [MaxLength(300, ErrorMessage = "We don't have space for that much information! 400 character limit.")]
        public string Summary { get; set; } = string.Empty;
        [Required(ErrorMessage = "A description is required.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "A picture is required.")]
        public string? ImgUrl { get; set; }
        public string? VideoUrl { get; set; }
        [Required(ErrorMessage ="A location is required")]
        public string Location { get; set; } = string.Empty;
        [Required(ErrorMessage = "A goal capital is required")]
        [Range(0, 10000000000, ErrorMessage = "Goal capital must be between 100 and 10000000000")]
        public decimal Goal { get; set; }
        [Range(0, 10000000000, ErrorMessage = "Funding must be between 100 and 10000000000")]
        public decimal Funding { get; set; }
        [Required(ErrorMessage = "A yield is required")]
        [Range(0, 10000000000, ErrorMessage = "Yield must be between 100 and 10000000000")]
        public decimal Yield { get; set; }
        public int Views { get; set; }
        [Required(ErrorMessage = "A category is required")]
        public string Categories {  get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Investment>? Investments { get; set; }
    }
}
