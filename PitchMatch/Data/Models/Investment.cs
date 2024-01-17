namespace PitchMatch.Data.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int? PitchId { get; set; }
        public int? UserId { get; set; }
        public Pitch? Pitch { get; set; }
        public User? User { get; set; }
    }
}
