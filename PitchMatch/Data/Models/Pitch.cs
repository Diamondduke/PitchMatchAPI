using Microsoft.Identity.Client;
using PitchMatch.Data.Models;

namespace PitchMatch.Data.Models
{
    public class Pitch
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImgUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal Goal { get; set; }
        public decimal Funding { get; set; }
        public decimal Yield { get; set; }
        public int Views { get; set; }
        public string Categories {  get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Investment>? Investments { get; set; }
    }
}
