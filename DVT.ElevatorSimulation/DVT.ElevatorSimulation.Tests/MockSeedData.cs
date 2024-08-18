using DVT.Domain.Entities;
using DVT.Domain.Enums;
using DVT.Infrastructure.Data;

namespace DVT.ElevatorSimulation.Tests;

public static class MockDataSeeder
{
    public static async Task SeedAsync(InMemoryDatabase context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.Elevators.Any())
        {
            var elevators = new List<Elevator>
            {
                new() { Id = 1, CurrentFloor = 0, State = ElevatorState.Idle, ElevatorType = ElevatorType.Standard, MaxPassengers = 10, MaxWeight = 1000 },
                new() { Id = 2, CurrentFloor = 1, State = ElevatorState.Idle, ElevatorType = ElevatorType.HighSpeed, MaxPassengers = 10, MaxWeight = 1000 },
                new() { Id = 3, CurrentFloor = 5, State = ElevatorState.MovingUp, ElevatorType = ElevatorType.Glass, MaxPassengers = 5, MaxWeight = 500 },
                new() { Id = 4, CurrentFloor = 10, State = ElevatorState.MovingDown, ElevatorType = ElevatorType.Freight, MaxPassengers = 2, MaxWeight = 2000 }
            };

            context.Elevators.AddRange(elevators);
            await context.SaveChangesAsync();
        }
    }
}
