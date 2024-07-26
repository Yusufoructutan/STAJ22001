using NSwag.Annotations;
using System;

namespace Ecommerce.DTO
{
    public class CartItemDto
    {
        [SwaggerIgnore]
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }

        
        
    }
}
