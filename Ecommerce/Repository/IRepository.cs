using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
