using Ecommerce.Bussines;
using Ecommerce.DTO;
using Ecommerce.Repository.Entity;
using Ecommerce.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OrderService : IOrderService
{
    private readonly IOrderBusiness _orderBusiness;

    public OrderService(IOrderBusiness orderBusiness)
    {
        _orderBusiness = orderBusiness;
    }

    public async Task<int> CreateOrderFromCartAsync(int userId)
    {
        return await _orderBusiness.CreateOrderFromCartAsync(userId);
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _orderBusiness.GetOrderByIdAsync(id);

        if (order == null)
        {
            return null; 
        }

        return new OrderDto
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                OrderItemId = oi.OrderItemId,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice 
            }).ToList()
        };
    }
}
