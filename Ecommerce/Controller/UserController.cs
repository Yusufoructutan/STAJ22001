using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        await _userService.RegisterAsync(registerDto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var message = await _userService.LoginAsync(loginDto);

        if (message == "Giriş başarılı! İyi alışverişler.")
        {
            return Ok(new { Message = message });
        }
        else
        {
            return Unauthorized(new { Message = message });
        }
    }

}
