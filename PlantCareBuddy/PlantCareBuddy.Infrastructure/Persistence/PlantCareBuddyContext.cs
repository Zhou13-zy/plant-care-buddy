using Microsoft.EntityFrameworkCore;
using PlantCareBuddy.Domain.Entities;

namespace PlantCareBuddy.Infrastructure.Persistence
{
    public class PlantCareBuddyContext : DbContext
    {
        public PlantCareBuddyContext(DbContextOptions<PlantCareBuddyContext> options)
            : base(options)
        { }

        public DbSet<Plant> Plants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Species)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.AcquisitionDate)
                    .IsRequired();

                entity.Property(p => p.Location)
                    .HasMaxLength(200);

                entity.Property(p => p.HealthStatus)
                    .IsRequired();

                entity.Property(p => p.Notes)
                    .HasMaxLength(1000);

                entity.Property(p => p.PrimaryImagePath)
                    .HasMaxLength(500);
            });
        }
    }
}