using OnlineShop.Services.ViewMoldels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services
{
    public interface IAddressService
    {
        void Create(AddressViewModel addressViewModel, long userId);
        IEnumerable<AddressViewModel> GetAddresses(long id);
    }
}
