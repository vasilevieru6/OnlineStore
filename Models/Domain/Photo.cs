using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Domain
{
    public class Photo : Entity
    {
        public string Path { get; set; }
        public string Type { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }

    }
}
