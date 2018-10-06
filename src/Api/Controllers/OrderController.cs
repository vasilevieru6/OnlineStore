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
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private long UserId => long.Parse(User.FindFirstValue("sub"));

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderViewModel orderViewModel)
        {
            _orderService.Create(orderViewModel, UserId);

            return Json(orderViewModel);
        }



    }
}