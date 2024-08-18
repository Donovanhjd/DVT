namespace DVT.Domain.Entities;

public class Passenger(int weight)
{
    public int Id { get; set; }
    public int Weight { get; set; } = weight;
    public int ElevatorId { get; set; }
    public Elevator? Elevator { get; set; }
}
