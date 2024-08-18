using DVT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DVT.Infrastructure.Data;

public class InMemoryDatabase(DbContextOptions<InMemoryDatabase> options) : DbContext(options)
{
    public DbSet<Elevator> Elevators { get; set; }
    public DbSet<Passenger> Passengers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Elevator>()
            .HasMany(e => e.Passengers)
            .WithOne(p => p.Elevator)
            .HasForeignKey(p => p.ElevatorId);

        base.OnModelCreating(modelBuilder);
    }
}
