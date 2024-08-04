public interface IUserService
{
    Task RegisterAsync(RegisterDto registerDto);
    Task AssignAdminRoleAsync(int userId);
    Task<string> LoginAsync(LoginDto loginDto);
}
