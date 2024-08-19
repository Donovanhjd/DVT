using DVT.Domain.Entities;
using DVT.Infrastructure.Data;
using DVT.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DVT.Infrastructure.Repositories;

public class ElevatorRepository(InMemoryDatabase database) : IRepository<Elevator>
{
    private readonly InMemoryDatabase _database = database;

    /// <summary>
    /// Retrieves all elevators from the in-memory database.
    /// </summary>
    /// <returns>An enumerable of all elevators.</returns>
    public async Task<IEnumerable<Elevator>> GetAll()
    {
        try
        {
            return await _database.Elevators
                .Include(e => e.Passengers)
                .ToListAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database update failed when retrieving Elevators.", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred when retrieving Elevators.", ex);
        }
    }

    /// <summary>
    /// Retrieves an elevator by its ID from the in-memory database.
    /// </summary>
    /// <param name="id">The ID of the elevator to retrieve.</param>
    /// <returns>The elevator with the specified ID.</returns>
    public async Task<Elevator> GetById(int id)
    {
        try
        {
            var elevator = await _database.Elevators
                .Include(e => e.Passengers)
                .FirstOrDefaultAsync(e => e.Id == id);

            return elevator;
        }
        catch (InvalidOperationException invOpEx)
        {
            throw new Exception(invOpEx.Message, invOpEx);
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database update failed when retrieving Elevator by ID.", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred when retrieving Elevator by ID.", ex);
        }
    }

    /// <summary>
    /// Adds a new elevator to the in-memory database.
    /// </summary>
    /// <param name="entity">The elevator to add.</param>
    public async Task Add(Elevator entity)
    {
        try
        {
            await _database.Elevators.AddAsync(entity);
            await _database.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database update failed when adding an Elevator.", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred when adding an Elevator.", ex);
        }
    }

    /// <summary>
    /// Adds multiple elevators to the in-memory database.
    /// </summary>
    /// <param name="entities">The elevators to add.</param>
    public async Task AddMultiple(IEnumerable<Elevator> entities)
    {
        try
        {
            await _database.Elevators.AddRangeAsync(entities);
            await _database.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database update failed when adding multiple Elevators.", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred when adding multiple Elevators.", ex);
        }
    }

    /// <summary>
    /// Updates an existing elevator in the in-memory database.
    /// </summary>
    /// <param name="entity">The elevator to update.</param>
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
        catch (InvalidOperationException invOpEx)
        {
            throw new Exception("Cannot update the Elevator because it was not found.", invOpEx);
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database update failed when updating the Elevator.", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred when updating the Elevator.", ex);
        }
    }

    /// <summary>
    /// Deletes an elevator by its ID from the in-memory database.
    /// </summary>
    /// <param name="id">The ID of the elevator to delete.</param>
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
        catch (InvalidOperationException invOpEx)
        {
            throw new Exception("Cannot delete the Elevator because it was not found.", invOpEx);
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("Database update failed when deleting the Elevator.", dbEx);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred when deleting the Elevator.", ex);
        }
    }
}
