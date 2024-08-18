namespace DVT.Application.Interfaces
{
    public interface IControlService
    {
        Task AddPassengers();
        Task CallElevator();
        Task RemovePassengers();
        Task StartSimulation();
    }
}