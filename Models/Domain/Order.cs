using System;
using System.Collections.Generic;

namespace OnlineShop.Models.Domain
{
    public enum OrderStatus { New, Hold, Shipped, Canceled };
    public class Order : Entity
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public int TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
