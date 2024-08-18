using DVT.Application.Interfaces;
using DVT.Domain.Entities;

namespace DVT.Application.Services;

public class ControlService(IPassengerService passengerService, IElevatorService elevatorService) : IControlService
{
    private readonly IPassengerService _passengerService = passengerService;
    private readonly IElevatorService _elevatorService = elevatorService;

    /// <summary>
    /// Handles the logic for calling an elevator.
    /// </summary>
    public async Task CallElevator()
    {
        var floorNumber = GetValidatedIntInput("Enter floor number to call elevator:", 1, 10);
        await _elevatorService.CallElevator(floorNumber)!;
    }

    /// <summary>
    /// Handles the logic for adding passengers to an elevator.
    /// </summary>
    public async Task AddPassengers()
    {
        var elevators = await _elevatorService?.GetElevators()!;

        var elevatorId = GetValidatedIntInput("Enter elevator number:", 1, elevators.Count());

        var elevator = await _elevatorService?.GetElevator(elevatorId)!;

        if (elevator is null)
        {
            Console.WriteLine("No elevators available.");
            return;
        }

        var numberOfPassengers = GetValidatedIntInput("Enter number of passengers:", 1, elevator?.MaxPassengers);
        var weightPerPassenger = GetValidatedIntInput("Enter weight per passenger (kg):", 1, elevator?.MaxWeight / elevator?.MaxPassengers);

        await _passengerService?.AddPassengers(elevatorId, numberOfPassengers, weightPerPassenger, elevator!.CurrentFloor)!;

        await MoveToDestinationFloor(elevator!);
    }

    private async Task MoveToDestinationFloor(Elevator elevator)
    {
        var destinationFloor = GetValidatedIntInput("Which floor are you moving to", 1, 10);
        await _elevatorService?.MoveElevatorToFloor(elevator!, destinationFloor)!;

        Console.WriteLine($"Elevator number: {elevator!.Id} has reached floor {destinationFloor}");
    }

    /// <summary>
    /// Handles the logic for removing passengers from an elevator.
    /// </summary>
    public async Task RemovePassengers()
    {
        var elevators = await _elevatorService?.GetElevators()!;

        var elevatorId = GetValidatedIntInput("Enter elevator ID:", 1, elevators.Count());

        var elevator = await _elevatorService?.GetElevator(elevatorId)!;

        if (elevator is null)
        {
            Console.WriteLine("No elevators available.");
            return;
        }

        var numberOfPassengers = GetValidatedIntInput("Enter number of passengers to remove:", 1, elevator?.Passengers?.Count);
        await _passengerService?.RemovePassengers(elevatorId, numberOfPassengers)!;
    }

    /// <summary>
    /// Validates integer input within a specified range.
    /// </summary>
    /// <param name="prompt">The prompt message to display.</param>
    /// <param name="minValue">The minimum valid value.</param>
    /// <param name="maxValue">The maximum valid value.</param>
    /// <returns>The validated integer input.</returns>
    private int GetValidatedIntInput(string prompt, int? minValue, int? maxValue)
    {
        int value;

        if (maxValue == 0)
        {
            return default;
        }

        do
        {
            Console.Write($"{prompt} ({minValue} - {maxValue}): ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out value) && value >= minValue && value <= maxValue)
            {
                break;
            }
            else
            {
                Console.WriteLine($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
            }
        } while (true);

        return value;
    }

    /// <summary>
    /// Starts the elevator simulation and handles user interaction.
    /// </summary>
    public async Task StartSimulation()
    {
        await _elevatorService.AddElevators();

        while (true)
        {
            Console.Clear();
            await _elevatorService.DisplayElevatorStatus();
            Console.WriteLine("Options:");
            Console.WriteLine("1. Call Elevator");
            Console.WriteLine("2. Add Passengers");
            Console.WriteLine("3. Remove Passengers");
            Console.WriteLine("4. Exit");

            var choice = GetValidatedIntInput($"Please choose an option between: ", 1, 4);

            switch (choice)
            {
                case 1:
                    await CallElevator();
                    break;
                case 2:
                    await AddPassengers();
                    break;
                case 3:
                    await RemovePassengers();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

