using Microsoft.EntityFrameworkCore;
using PitchMatch.Data.Models;

namespace PitchMatch.Data
{
    public class PitchMatchDbContext : DbContext
    {
        public PitchMatchDbContext(DbContextOptions<PitchMatchDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Pitch> Pitch { get; set; } = default!;
        public DbSet<Investment> Investment { get; set; } = default!;
        public DbSet<PersonalData> PersonalData { get; set; } = default!;
    }
}
