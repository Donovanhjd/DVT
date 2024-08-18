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
        //TODO: Remove passengers Logic
    }
}