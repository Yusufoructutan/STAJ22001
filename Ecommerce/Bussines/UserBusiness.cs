using Ecommerce.Repository.Entity;
using Ecommerce.Repository;
using Ecommerce.Repository.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IRepository<User> _userRepository;

        public UserBusiness(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var users = await _userRepository.GetAllAsync();
            return users.FirstOrDefault(u => u.Username == username);
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = passwordHash;
            await _userRepository.AddAsync(user);
        }





        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }





        public async Task UpdateUserAsync(User user)
        {
            // Kullanıcıyı güncelle
            await _userRepository.UpdateAsync(user);
        }
    }
}
