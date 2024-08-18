using DVT.Application.Interfaces;
using DVT.Domain.Entities;
using DVT.Infrastructure.Interfaces;

namespace DVT.Application.Services;

public class PassengerService(IRepository<Elevator> elevatorRepository) : IPassengerService
{
    private readonly IRepository<Elevator> _elevatorRepo = elevatorRepository;

    /// <summary>
    /// Adds passengers to the specified elevator and updates its state in the repository.
    /// </summary>
    /// <param name="elevatorId">The ID of the elevator.</param>
    /// <param name="numberOfPassengers">The number of passengers to add.</param>
    /// <param name="weightPerPassenger">The weight of each passenger.</param>
    /// <param name="requestedFloor">The floor number to which the passengers are requesting.</param>
    public async Task AddPassengers(int elevatorId, int numberOfPassengers, int weightPerPassenger, int requestedFloor)
    {
        var elevator = await _elevatorRepo.GetById(elevatorId);
        if (GetValidatedIntInput("Elevator not found.", elevator == null)) return;

        AddPassengersToElevator(elevator!, numberOfPassengers, weightPerPassenger);
        var totalPassengerWeight = elevator?.Passengers?.Sum(rec => rec.Weight);

        if (GetValidatedIntInput("Adding these passengers would exceed the weight limit.", totalPassengerWeight > elevator!.MaxWeight)) return;
        if (GetValidatedIntInput("Elevator is at full capacity.", (elevator?.Passengers?.Count ?? 0 + numberOfPassengers) > elevator!.MaxPassengers)) return;

        await _elevatorRepo.Update(elevator);

        Console.WriteLine($"{numberOfPassengers} passengers added to elevator {elevatorId}.");
        Console.WriteLine($"Total weight: {totalPassengerWeight} kg");
    }

    /// <summary>
    /// Adds a specified number of passengers with a given weight to an elevator.
    /// </summary>
    /// <param name="elevator">The elevator to which passengers are added.</param>
    /// <param name="numberOfPassengers">The number of passengers to add.</param>
    /// <param name="weightPerPassenger">The weight of each passenger.</param>
    private void AddPassengersToElevator(Elevator elevator, int numberOfPassengers, int weightPerPassenger)
    {
        for (int passenger = 1; passenger < numberOfPassengers + 1; passenger++)
        {
            elevator.Passengers?.Add(new(weightPerPassenger));
        }
    }

    /// <summary>
    /// Removes passengers from the specified elevator.
    /// </summary>
    /// <param name="elevatorId">The ID of the elevator.</param>
    /// <param name="numberOfPassengers">The number of passengers to remove.</param>
    public async Task RemovePassengers(int elevatorId, int numberOfPassengers)
    {
        var elevator = await _elevatorRepo.GetById(elevatorId);

        if (GetValidatedIntInput("Elevator not found.", elevator == null)) return;
        if (GetValidatedIntInput("Not enough passengers to remove.", elevator?.Passengers?.Count <= numberOfPassengers)) return;

        elevator!.Passengers = elevator.Passengers?
            .Take(elevator.Passengers.Count - numberOfPassengers)
            .ToList();

        await _elevatorRepo.Update(elevator);

        Console.WriteLine($"{numberOfPassengers} passengers removed from elevator {elevatorId}.");
    }

    /// <summary>
    /// Validates input and returns a boolean value indicating if the condition was met.
    /// </summary>
    /// <param name="prompt">The prompt message to display.</param>
    /// <param name="condition">The condition to validate.</param>
    /// <returns>A boolean value indicating if the condition was met.</returns>
    private bool GetValidatedIntInput(string prompt, bool condition = false)
    {
        if (condition)
        {
            Console.WriteLine(prompt);
            return true;
        }

        return condition;
    }
}