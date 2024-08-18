namespace DVT.Application.Interfaces;

public interface IPassengerService
{
    Task AddPassengers(int elevatorId, int numberOfPassengers, int weightPerPassenger, int requestedFloor);
    Task RemovePassengers(int elevatorId, int numberOfPassengers);
}