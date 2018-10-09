using OnlineShop.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.ViewMoldels
{
    public class OrderInfoViewModel : BaseViewModel
    {
        public int TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public Address Address { get; set; }
    }
}
