using DVT.Application.Interfaces;
using DVT.Domain.Entities;
using DVT.Domain.Enums;
using DVT.Infrastructure.Interfaces;

namespace DVT.Application.Services;

public class ElevatorService(IRepository<Elevator> elevatorRepository) : IElevatorService
{
    private readonly IRepository<Elevator> _elevatorRepository = elevatorRepository;

    private async Task<Elevator> GetAvailableElevator(int floorNumber)
    {
        //TODO: Return All elevators that are closest to floor
    }

    public async Task AddElevators()
    {
        //TODO: Add elevators to inmemory DB
    }

    public async Task<Elevator> GetElevator(int id)
    {
        //TODO: Get elevator for id
    }

    public async Task CallElevator(int floorNumber)
    {
        //TODO: Call elevator to floornumber
    }

    public async Task MoveElevatorToFloor(Elevator elevator, int destinationFloor)
    {
        //TODO: Move elevator to destination floor
    }

    private void ElevatorSpeed(ElevatorType elevatorType)
    {
        //TODO: Set up speed per elevator
    }

    public async Task<IEnumerable<Elevator>> GetElevators()
    {
        //TODO: Get all elevators
    }

    public async Task DisplayElevatorStatus()
    {
        //TODO: Display Elevator status with consoleapp
    }
}
