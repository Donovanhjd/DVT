using DVT.Application.Interfaces;

namespace DVT.Application.Services;

public class ControlService(IPassengerService passengerService, IElevatorService elevatorService) : IControlService
{
    private readonly IPassengerService _passengerService = passengerService;
    private readonly IElevatorService _elevatorService = elevatorService;

    public async Task CallElevator()
    {
        //TODO: Call all elevators
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
        //TODO: Vaidate input
    }

    public async Task StartSimulation()
    {
        //TODO: Start application with options
    }
}

