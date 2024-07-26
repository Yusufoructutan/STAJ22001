public interface IBusiness<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity); // Add this line
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
