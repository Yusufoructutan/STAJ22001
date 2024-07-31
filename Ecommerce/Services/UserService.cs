using Ecommerce.Business;
using Ecommerce.Repository.Entity;
using Ecommerce.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            Role = "User" // Varsayılan rol
        };
        await _userBusiness.RegisterUserAsync(user, registerDto.Password);
    }


    public async Task AssignAdminRoleAsync(int userId)
    {
        var user = await _userBusiness.GetUserByIdAsync(userId);
        if (user != null)
        {
            user.Role = "Admin"; // Rolü admin olarak değiştir
            await _userBusiness.UpdateUserAsync(user); // Kullanıcıyı güncelle
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
                return "Kullanıcı bulunamadı."; // Hata mesajı
            }

            var token = GenerateJwtToken(user);
            return token; // Başarıyla oluşturulan token
        }

        return "Hatalı kullanıcı adı veya şifre."; // Hata mesajı
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
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]); // Konfigürasyondan anahtarı al
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            Issuer = _configuration["Jwt:Issuer"], // Konfigürasyondan Issuer al
            Audience = _configuration["Jwt:Audience"] // Konfigürasyondan Audience al
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
