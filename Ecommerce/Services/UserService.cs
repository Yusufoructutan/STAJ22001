using Ecommerce.Business;
using Ecommerce.DTO;
using Ecommerce.Repository.Entity;
using Ecommerce.Repository.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IUserBusiness _userBusiness;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserBusiness userBusiness, IHttpContextAccessor httpContextAccessor)
    {
        _userBusiness = userBusiness;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task RegisterAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            CreatedDate = DateTime.UtcNow
        };
        await _userBusiness.RegisterUserAsync(user, registerDto.Password);
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

            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return "Giriş başarılı! İyi alışverişler."; // Başarı mesajı
        }
        return "Hatalı kullanıcı adı veya şifre."; // Hata mesajı
    }

   
}
