using System.Collections.Generic;

namespace OnlineShop.Services.ViewMoldels
{
    public class ProductViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public ICollection<PhotoViewModel> Photos { get; set; }
    }
}
