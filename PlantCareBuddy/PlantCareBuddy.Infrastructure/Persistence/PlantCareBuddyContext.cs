using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Domain.Entities;

namespace PlantCareBuddy.Infrastructure.Persistence
{
    public class PlantCareBuddyContext : DbContext
    {
        public PlantCareBuddyContext(DbContextOptions<PlantCareBuddyContext> options)
            : base(options)
        {}
        public DbSet<Plant> Plants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Plant entity
            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Species).IsRequired();
                entity.Property(e => e.AcquisitionDate).IsRequired();
                entity.Property(e => e.HealthStatus).IsRequired();
            });
        }
    }
}