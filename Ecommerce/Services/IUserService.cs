using Ecommerce.DTO;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<String> LoginAsync(LoginDto loginDto);
    }
}
