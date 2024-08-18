using DVT.Domain.Entities;
using DVT.Domain.Enums;
using DVT.Infrastructure.Data;
using DVT.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DVT.ElevatorSimulation.Tests;

public class ElevatorRepositoryTests : IAsyncLifetime
{
    private readonly ElevatorRepository _repository;
    private readonly InMemoryDatabase _context;

    public ElevatorRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<InMemoryDatabase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new InMemoryDatabase(options);
        _repository = new ElevatorRepository(_context);
    }

    public async Task InitializeAsync()
    {
        await MockDataSeeder.SeedAsync(_context);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Update_ShouldUpdateElevator()
    {
        // Arrange
        var elevator = await _repository.GetById(1);
        elevator.CurrentFloor = 7;
        elevator.State = ElevatorState.MovingDown;

        // Act
        await _repository.Update(elevator);
        var result = await _repository.GetById(elevator.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(elevator.CurrentFloor, result.CurrentFloor);
        Assert.Equal(elevator.State, result.State);
    }

    [Fact]
    public async Task AddElevators_ShouldAddElevatorsToRepository()
    {
        // Arrange
        var newElevator = new Elevator
        {
            Id = 5,
            CurrentFloor = 2,
            State = ElevatorState.Idle,
            ElevatorType = ElevatorType.Standard,
            MaxPassengers = 10,
            MaxWeight = 1000
        };

        // Act
        await _repository.Add(newElevator);

        // Assert
        var result = await _repository.GetById(newElevator.Id);
        Assert.NotNull(result);
        Assert.Equal(newElevator.Id, result.Id);
        Assert.Equal(newElevator.CurrentFloor, result.CurrentFloor);
        Assert.Equal(newElevator.State, result.State);
        Assert.Equal(newElevator.ElevatorType, result.ElevatorType);
        Assert.Equal(newElevator.MaxPassengers, result.MaxPassengers);
        Assert.Equal(newElevator.MaxWeight, result.MaxWeight);
    }

    [Fact]
    public async Task Delete_ShouldRemoveElevator()
    {
        // Arrange
        var elevator = await _repository.GetById(2);
        Assert.NotNull(elevator);

        // Act
        await _repository.Delete(elevator.Id);
        var result = await _repository.GetById(elevator.Id);

        // Assert
        Assert.Null(result);
    }
}