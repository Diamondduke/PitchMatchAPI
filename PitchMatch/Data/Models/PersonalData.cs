using System.Net;

namespace PitchMatch.Data.Models
{
    public class PersonalData
    {
        public int Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PersonNr { get; set; }
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsVerified { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
