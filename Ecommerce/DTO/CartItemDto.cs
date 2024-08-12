using NSwag.Annotations;
using System;

namespace Ecommerce.DTO
{
    public class CartItemDto
    {
       
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
      

        
        
    }
}
