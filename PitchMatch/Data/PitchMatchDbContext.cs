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
        public DbSet<User> Pitch { get; set; } = default!;
        public DbSet<User> Investment { get; set; } = default!;
        public DbSet<User> PersonalData { get; set; } = default!;
    }
}
