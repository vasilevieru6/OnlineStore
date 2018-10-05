using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.ViewMoldels
{
    public class ProductViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
