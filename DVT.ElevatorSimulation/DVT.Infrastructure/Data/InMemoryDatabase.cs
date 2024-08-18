using DVT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DVT.Infrastructure.Data;

/// <summary>
/// Initializes a new instance of the <see cref="InMemoryDatabase"/> class.
/// </summary>
/// <param name="options">The options for configuring the context.</param>
public class InMemoryDatabase(DbContextOptions<InMemoryDatabase> options) : DbContext(options)
{
    public DbSet<Elevator> Elevators { get; set; }
    public DbSet<Passenger> Passengers { get; set; }

    /// <summary>
    /// Configures the model for the context, including relationships and constraints.
    /// </summary>
    /// <param name="modelBuilder">The builder used to configure the model.</param
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Elevator>()
            .HasMany(e => e.Passengers)
            .WithOne(p => p.Elevator)
            .HasForeignKey(p => p.ElevatorId);

        base.OnModelCreating(modelBuilder);
    }
}
