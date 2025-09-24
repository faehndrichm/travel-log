using Microsoft.EntityFrameworkCore;

namespace journey_service.Entities
{
    public class JourneyContext : DbContext
    {
        public JourneyContext(DbContextOptions<JourneyContext> options)
             : base(options)
        {
        }

        public DbSet<Journey> Journeys { get; set; } = null!;
        public DbSet<Spot> Spots { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Journey>()
                .HasMany(j => j.Spots)
                .WithOne(p => p.Journey)
                .HasForeignKey(p => p.JourneyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}