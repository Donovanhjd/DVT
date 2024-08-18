using DVT.Domain.Entities;
using DVT.Infrastructure.Data;
using DVT.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DVT.Infrastructure.Repositories;

public class ElevatorRepository(InMemoryDatabase database) : IRepository<Elevator>
{
    private readonly InMemoryDatabase _database = database;

    public async Task<IEnumerable<Elevator>> GetAll()
    {
        try
        {
            return await _database.Elevators
                .Include(e => e.Passengers)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Not able to find Elevators", ex);
        }
    }

    public async Task<Elevator> GetById(int id)
    {
        try
        {
            var elevator = await _database.Elevators
                .Include(e => e.Passengers)
                .FirstOrDefaultAsync(e => e.Id == id);

            return elevator ?? throw new Exception($"Elevator with id {id} was not found.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Not able to find Elevator with id {id}.", ex);
        }
    }

    public async Task Add(Elevator entity)
    {
        try
        {
            await _database.Elevators.AddAsync(entity);
            await _database.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Not able to add Elevator.", ex);
        }
    }

    public async Task AddMultiple(IEnumerable<Elevator> entities)
    {
        try
        {
            await _database.Elevators.AddRangeAsync(entities);
            await _database.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Not able to add Elevators.", ex);
        }
    }

    public async Task Update(Elevator entity)
    {
        try
        {
            var existingElevator = await GetById(entity.Id);
            if (existingElevator is not null)
            {
                _database.Entry(existingElevator).CurrentValues.SetValues(entity);

                await _database.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Not able to update Elevator", ex);
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var elevator = await GetById(id);
            if (elevator is not null)
            {
                _database.Elevators.Remove(elevator);
                await _database.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Not able to delete Elevator", ex);
        }
    }
}