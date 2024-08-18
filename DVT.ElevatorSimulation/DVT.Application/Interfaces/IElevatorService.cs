using DVT.Domain.Entities;

namespace DVT.Application.Interfaces;

public interface IElevatorService
{
    Task AddElevators();
    Task CallElevator(int floorNumber);
    Task DisplayElevatorStatus();
    Task<Elevator> GetElevator(int id);
    Task<IEnumerable<Elevator>> GetElevators();
    Task MoveElevatorToFloor(Elevator elevator, int destinationFloor);
}