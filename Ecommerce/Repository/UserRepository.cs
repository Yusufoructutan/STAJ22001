using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Repository;
using Ecommerce.Repository.Entity;
using Ecommerce.Repository.Models;
using System.Linq.Expressions;

public class UserRepository : IRepository<User>
{
    private readonly ECommerceContext _context;
    private readonly DbSet<User> _dbSet;

    public UserRepository(ECommerceContext context)
    {
        _context = context;
        _dbSet = _context.Set<User>();

    }
   

    public async Task<User> GetAsync(Expression<Func<User, bool>> predicate)
    {
        return await _dbSet.SingleOrDefaultAsync(predicate);
    }
    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public IQueryable<User> GetAll()
    {
        return _context.Users;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

  
}
