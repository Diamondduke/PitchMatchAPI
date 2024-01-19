using Microsoft.EntityFrameworkCore;
using PitchMatch.Data.Models;

namespace PitchMatch.Data
{
    public class PitchMatchDbContext : DbContext
    {
        public PitchMatchDbContext(DbContextOptions<PitchMatchDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Pitch> Pitch { get; set; } = default!;
        public DbSet<Investment> Investment { get; set; } = default!;
        public DbSet<PersonalData> PersonalData { get; set; } = default!;
    }
}
