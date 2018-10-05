using System;
using System.Collections.Generic;

namespace OnlineShop.Models.Domain
{
    public class ShoppingCart : Entity
    {
        public DateTime CreatedDate { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public ICollection<ShoppingCartItem> CartItems { get; set; }
    }
}
