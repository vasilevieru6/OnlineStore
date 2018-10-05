namespace OnlineShop.Models.Domain
{
    public class OrderLine : Entity
    {
        public int Quatity { get; set; }
        public int Price { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long OrderId { get; set; }   
        public Order Order { get; set; }
    }
}
