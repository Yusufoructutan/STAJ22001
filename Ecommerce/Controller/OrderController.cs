using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    private int GetCurrentUserId()
    {
        // Kullanıcı kimliğini almak için ASP.NET Core Identity kullanıyorsanız:
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userId, out int id))
        {
            return id;
        }
        throw new InvalidOperationException("Kullanıcı kimliği alınamadı.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        try
        {
            var userId = GetCurrentUserId(); // Kullanıcının ID'sini alın
            var orderId = await _orderService.CreateOrderFromCartAsync(userId);

            // Başarıyla oluşturulan siparişi döndürün
            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = orderId },
                new
                {
                    OrderId = orderId,
                    Message = "Sipariş başarıyla oluşturuldu."
                });
        }
        catch (InvalidOperationException ex)
        {
            // Hata durumunda uygun yanıt döndürün
            return BadRequest(new
            {
                ErrorMessage = ex.Message
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var orderDto = await _orderService.GetOrderByIdAsync(id);
        if (orderDto == null)
        {
            return NotFound();
        }
        return Ok(orderDto);
    }
}
