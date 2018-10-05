using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.ViewMoldels
{
    public class CartItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public long ShoppingCartId { get; set; }
        public long ProductId { get; set; }
        
    }
}
