using DVT.Domain.Enums;

namespace DVT.Domain.Entities;

/// <summary>
/// Represents an elevator in the building.
/// </summary>
/// <remarks>
/// <see cref="Id"/>: Unique identifier for the elevator.
/// <see cref="CurrentFloor"/>: The current floor of the elevator.
/// <see cref="MaxPassengers"/>: The maximum number of passengers the elevator can hold.
/// <see cref="MaxWeight"/>: The maximum weight capacity of the elevator.
/// <see cref="State"/>: The current state of the elevator (e.g., Idle, MovingUp, MovingDown).
/// <see cref="ElevatorType"/>: The type of the elevator (e.g., Standard, HighSpeed, Glass, Freight).
/// <see cref="Passengers"/>: The list of passengers currently in the elevator.
/// </remarks>
public class Elevator
{
    public int Id { get; set; }
    public int CurrentFloor { get; set; }
    public int MaxPassengers { get; set; }
    public int MaxWeight { get; set; }
    public ElevatorState State { get; set; }
    public ElevatorType ElevatorType { get; set; }
    public List<Passenger>? Passengers { get; set; } = [];
}
