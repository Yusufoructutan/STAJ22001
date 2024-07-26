using Ecommerce.Repository.Entity;
using Ecommerce.Repository.Models;
using System.Threading.Tasks;

namespace Ecommerce.Business
{
    public interface IUserBusiness
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username); // Yeni metod eklendi
        Task RegisterUserAsync(User user, string password);
        Task<bool> ValidateUserAsync(string username, string password);
    }
}
