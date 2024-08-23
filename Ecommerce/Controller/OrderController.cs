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
            var userId = GetCurrentUserId();
            var orderId = await _orderService.CreateOrderFromCartAsync(userId);

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
            return BadRequest(new
            {
                ErrorMessage = ex.Message
            });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetUserOrders()
    {
        try
        {
            var userId = GetCurrentUserId();
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
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
