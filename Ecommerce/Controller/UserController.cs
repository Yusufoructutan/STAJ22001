using Ecommerce.DTO;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // Kullanıcı kaydı
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userService.RegisterAsync(registerDto);
        return Ok("Kayıt başarılı.");
    }

    // Kullanıcı girişi ve JWT token alma
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var token = await _userService.LoginAsync(loginDto);

        if (token.StartsWith("Hatalı"))
        {
            return Unauthorized(token);
        }

        return Ok(new { Token = token });
    }
}
