namespace DVT.Infrastructure.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task Add(T entity);
    Task AddMultiple(IEnumerable<T> entities);
    Task Update(T entity);
    Task Delete(int id);
}
