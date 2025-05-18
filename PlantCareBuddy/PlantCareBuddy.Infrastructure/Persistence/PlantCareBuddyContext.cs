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
        public DbSet<CareEvent> CareEvents { get; set; }
        public DbSet<HealthObservation> HealthObservations { get; set; }



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

                entity.Property(e => e.NextHealthCheckDate);

                entity.Property(p => p.Notes)
                    .HasMaxLength(1000);

                entity.Property(p => p.PrimaryImagePath)
                    .HasMaxLength(500);
            });
            modelBuilder.Entity<CareEvent>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.EventType)
                    .IsRequired();

                entity.Property(e => e.EventDate)
                    .IsRequired();

                entity.Property(e => e.Notes)
                    .HasMaxLength(1000);

                entity.Property(e => e.BeforeImagePath)
                    .HasMaxLength(500);

                entity.Property(e => e.AfterImagePath)
                    .HasMaxLength(500);

                entity.HasOne(e => e.Plant)
                    .WithMany(p => p.CareEvents)
                    .HasForeignKey(e => e.PlantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<HealthObservation>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ObservationDate)
                    .IsRequired();

                entity.Property(p => p.HealthStatus)
                    .IsRequired();

                entity.Property(e => e.Notes)
                    .IsRequired();

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(500);

                entity.HasOne(e => e.Plant)
                    .WithMany(p => p.HealthObservations)
                    .HasForeignKey(e => e.PlantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}