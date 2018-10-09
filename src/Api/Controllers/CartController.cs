using System.Collections.Generic;
using System.Linq;
using OnlineShop.Services;
using OnlineShop.Services.ViewMoldels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.Domain;
using System;
using System.Security.Claims;

namespace OnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private long UserId => long.Parse(User.FindFirstValue("sub"));

        [HttpGet("products")]        
        public IList<CartItemViewModel> Get()
        {      
            return _cartService.GetAllProducts();
        }

        [HttpPatch("cartItem/{id}")]
        public IActionResult SetCartItemQuantity([FromBody] QuantityCartItemViewModel quantityCartItemViewModel, long id)
        {
            _cartService.UpdateCartItemQuantity(quantityCartItemViewModel);

            return Json(quantityCartItemViewModel);
        }

        [HttpPost("product")]
        public IActionResult CreateCartItem([FromBody] QuantityCartItemViewModel cartItemViewModel)
        {
            _cartService.AddProductToCart(cartItemViewModel, UserId);
            return Json(cartItemViewModel);
        }

        [HttpDelete("product/{id}")]
        public void DeleteProductFromCart(long id)
        {
            _cartService.DeleteCartProduct(id);
        }

        [HttpGet("product/{id}")]
        public int GetProductPrice(long id)
        {
            return _cartService.GetCartProductPrice(id);
        }
    }
}