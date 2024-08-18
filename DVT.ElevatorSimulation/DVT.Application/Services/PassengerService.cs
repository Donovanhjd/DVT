using DVT.Application.Interfaces;
using DVT.Domain.Entities;
using DVT.Infrastructure.Interfaces;

namespace DVT.Application.Services;

public class PassengerService(IRepository<Elevator> elevatorRepository) : IPassengerService
{
    private readonly IRepository<Elevator> _elevatorRepo = elevatorRepository;

    public async Task AddPassengers(int elevatorId, int numberOfPassengers, int weightPerPassenger, int requestedFloor)
    {
        //TODO: Addpassengers logic
    }

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