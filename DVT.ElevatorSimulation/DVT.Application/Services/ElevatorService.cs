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
        var elevators = await GetElevators();

        return elevators.Where(rec => rec.State == ElevatorState.Idle)
            .OrderBy(e => Math.Abs(e.CurrentFloor - floorNumber))
            .FirstOrDefault()!;
    }

    public async Task AddElevators()
    {
        List<Elevator> elevators =
        [
            new Elevator { Id = 1, CurrentFloor = 0, State = ElevatorState.Idle, ElevatorType = ElevatorType.Standard, MaxPassengers = 10, MaxWeight = 1000 },
            new Elevator { Id = 2, CurrentFloor = 5, State = ElevatorState.Idle, ElevatorType = ElevatorType.HighSpeed, MaxPassengers = 10, MaxWeight = 1000 }
        ];

        await _elevatorRepository.AddMultiple(elevators);
    }

    public async Task<Elevator> GetElevator(int id) =>
        await _elevatorRepository.GetById(id);

    public async Task CallElevator(int floorNumber)
    {
        var availableElevator = await GetAvailableElevator(floorNumber);

        if (availableElevator is not null)
        {
            await MoveElevatorToFloor(availableElevator, floorNumber);
        }
        else
        {
            Console.WriteLine("Please wait for an available elevator.");
        }
    }

    public async Task MoveElevatorToFloor(Elevator elevator, int destinationFloor)
    {
        elevator.State = elevator.CurrentFloor < destinationFloor ? ElevatorState.MovingUp : ElevatorState.MovingDown;

        while (elevator.CurrentFloor != destinationFloor)
        {
            if (elevator.State == ElevatorState.MovingUp)
            {
                elevator.CurrentFloor++;

                Console.Clear();
                await DisplayElevatorStatus();
            }
            else if (elevator.State == ElevatorState.MovingDown)
            {
                elevator.CurrentFloor--;

                Console.Clear();
                await DisplayElevatorStatus();
            }

            ElevatorSpeed(elevator.ElevatorType);
        }

        elevator.State = ElevatorState.Idle;
        await _elevatorRepository.Update(elevator);
    }

    private void ElevatorSpeed(ElevatorType elevatorType)
    {
        switch (elevatorType)
        {
            case ElevatorType.Standard:
                Thread.Sleep(700);
                break;
            case ElevatorType.Freight:
                Thread.Sleep(600);
                break;
            case ElevatorType.HighSpeed:
                Thread.Sleep(500);
                break;
        }
    }

    public async Task<IEnumerable<Elevator>> GetElevators() =>
        await _elevatorRepository.GetAll();

    public async Task DisplayElevatorStatus()
    {
        var elevators = await GetElevators();
        elevators.OrderBy(e => e.Id);

        if (elevators is null)
        {
            Console.WriteLine("No elevators available.");
            return;
        }

        foreach (var elevator in elevators!)
        {
            Console.WriteLine($"Elevator Number: {elevator.Id}");
            Console.WriteLine($"Current Floor: {elevator.CurrentFloor}");
            Console.WriteLine($"People: {elevator?.Passengers?.Count ?? 0}/{elevator!.MaxPassengers}");
            Console.WriteLine($"Weight: {elevator?.Passengers?.Sum(rec => rec.Weight) ?? 0} kg/{elevator!.MaxWeight} kg");
            Console.WriteLine($"Type: {elevator.ElevatorType}");
            Console.WriteLine($"State: {elevator.State}");
            Console.WriteLine();
        }
    }
}
