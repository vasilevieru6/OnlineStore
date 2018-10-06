using OnlineShop.Services.ViewMoldels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services
{
    public interface IOrderService
    {
        void Create(OrderViewModel orderViewModel, long userId);
    }
}
