using DVT.Domain.Enums;

namespace DVT.Domain.Entities;

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
