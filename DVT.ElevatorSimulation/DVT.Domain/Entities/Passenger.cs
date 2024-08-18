namespace DVT.Domain.Entities;

/// <summary>
/// Represents a passenger in the elevator.
/// </summary>
/// <remarks>
/// <see cref="Id"/>: Unique identifier for the passenger.
/// <see cref="Weight"/>: The weight of the passenger.
/// <see cref="ElevatorId"/>: The ID of the elevator the passenger is in.
/// <see cref="Elevator"/>: The elevator that the passenger is in.
/// </remarks>
public class Passenger(int weight)
{
    public int Id { get; set; }
    public int Weight { get; set; } = weight;
    public int ElevatorId { get; set; }
    public Elevator? Elevator { get; set; }
}
