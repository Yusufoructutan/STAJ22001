using Ecommerce.Business;
using Ecommerce.Repository.Entity;
using Ecommerce.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IUserBusiness _userBusiness;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public UserService(IUserBusiness userBusiness, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _userBusiness = userBusiness;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public async Task RegisterAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            CreatedDate = DateTime.UtcNow,
            Role = "User" 
        };
        await _userBusiness.RegisterUserAsync(user, registerDto.Password);
    }

    public async Task AssignAdminRoleAsync(int userId)
    {
        var user = await _userBusiness.GetUserByIdAsync(userId);
        if (user != null)
        {
            user.Role = "Admin"; 
            await _userBusiness.UpdateUserAsync(user); 
        }
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var isValidUser = await _userBusiness.ValidateUserAsync(loginDto.Username, loginDto.Password);

        if (isValidUser)
        {
            var user = await _userBusiness.GetUserByUsernameAsync(loginDto.Username);
            if (user == null)
            {
                return "Hatalı kullanıcı adı veya şifre."; 
            }

            var token = GenerateJwtToken(user);
            return token; 
        }

        return "Hatalı kullanıcı adı veya şifre."; 
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]); 
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1), 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            Issuer = _configuration["Jwt:Issuer"], 
            Audience = _configuration["Jwt:Audience"] 
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
