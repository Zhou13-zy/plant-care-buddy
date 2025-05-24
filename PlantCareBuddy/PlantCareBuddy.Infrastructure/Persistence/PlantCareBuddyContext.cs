using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public DbSet<Reminder> Reminders { get; set; }



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

                entity.HasIndex(e => e.PlantId);
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

                entity.HasIndex(e => e.PlantId);
            });
            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(r => r.Description)
                    .HasMaxLength(500);

                entity.Property(r => r.StrategyParameters)
                    .HasMaxLength(1000);

                entity.Property(r => r.DueDate)
                    .IsRequired();

                entity.Property(r => r.IsCompleted)
                    .IsRequired();

                entity.Property(r => r.Intensity)
                    .IsRequired();

                entity.HasOne(r => r.Plant)
                    .WithMany(p => p.Reminders)
                    .HasForeignKey(r => r.PlantId)
                    .OnDelete(DeleteBehavior.Cascade);

                // RecurrencePattern as owned type
                entity.OwnsOne(r => r.Recurrence, recurrence =>
                {
                    recurrence.Property(rp => rp.Type).IsRequired();
                    recurrence.Property(rp => rp.Interval).IsRequired();
                    recurrence.Property(rp => rp.EndDate);
                    recurrence.Property(rp => rp.OccurrenceCount);
                    recurrence.Property(rp => rp.DaysOfWeek)
                        .HasConversion(
                            v => string.Join(',', v),
                            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(d => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), d))
                                  .ToArray())
                        .Metadata.SetValueComparer(new ValueComparer<DayOfWeek[]>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToArray()
                        ));
                    recurrence.Property(rp => rp.DayOfMonth);
                });

                entity.HasIndex(r => r.PlantId);
                entity.HasIndex(r => r.DueDate);
                entity.HasIndex(r => r.IsCompleted);
                entity.HasIndex(r => r.StrategyId);
            });
        }

    }
}