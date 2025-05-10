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
            // Only add advanced configuration here if needed.
            // For example, relationships, indexes, or table names.
            // No need to repeat .IsRequired() or .HasMaxLength() for Plant properties.
        }
    }
}