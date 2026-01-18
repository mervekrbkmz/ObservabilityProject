using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.OrderServices
{
    public record OrderCreateRequestDto
    {
        //record : ordercretatedimmuttable maplendiği anda bir daha değiştirilmesin
        public string UserId { get; init; } = null!;
        List<OrderItemDto> OrderItems { get; set; } = new();
    }
    public record OrderItemDto
    {
         public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}