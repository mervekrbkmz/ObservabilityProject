namespace Order.API.OrderServices
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }=null!;
        public DateTime Created { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    public enum OrderStatus:byte
    {
        Pending = 4,
        Processing = 3,
        Completed = 2,
        Cancelled = 1,
        Failed = 0
    }
    public class OrderItem {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}