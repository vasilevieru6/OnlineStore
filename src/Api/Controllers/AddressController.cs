using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services;
using OnlineShop.Services.ViewMoldels;

namespace OnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        private long UserId => long.Parse(User.FindFirstValue("sub"));

        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressViewModel addressViewModel)
        {
            _addressService.Create(addressViewModel, UserId);

            return Json(addressViewModel);
        }

        [HttpGet]
        public IEnumerable<AddressViewModel> Get()
        {
            return _addressService.GetAddresses(UserId);
        }
    }
}