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
    }
}