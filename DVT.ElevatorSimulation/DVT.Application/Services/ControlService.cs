using DVT.Application.Interfaces;

namespace DVT.Application.Services;

public class ControlService(IPassengerService passengerService, IElevatorService elevatorService) : IControlService
{
    private readonly IPassengerService _passengerService = passengerService;
    private readonly IElevatorService _elevatorService = elevatorService;

    public async Task CallElevator()
    {
        var floorNumber = GetValidatedIntInput("Enter floor number to call elevator:", 1, 10);
        await _elevatorService.CallElevator(floorNumber)!;
    }

    public async Task AddPassengers()
    {
        //TODO: Call add passenger service
    }

    public async Task RemovePassengers()
    {
        //TODO: call remove passenger from service
    }

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

