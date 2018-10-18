using System.Collections.Generic;

namespace OnlineShop.Models.Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
        public ICollection<ProductCharacteristics> Characteristics { get; set; }
        public ICollection<ShoppingCartItem> CartItems { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
