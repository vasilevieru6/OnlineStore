using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.ViewMoldels
{
    public class PagedViewModel<T>
    {
        public int Count { get; set; }
        public IList<T> Data { get; set; }
    }
}
