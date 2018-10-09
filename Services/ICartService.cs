using OnlineShop.Services.ViewMoldels;
using OnlineShop.Models.Domain;
using System.Collections.Generic;

namespace OnlineShop.Services
{
    public interface ICartService
    {
        IList<CartItemViewModel> GetAllProducts();
        void UpdateCartItemQuantity(QuantityCartItemViewModel cartItem);
        void AddProductToCart(QuantityCartItemViewModel cartItem, long id);
        int GetCartProductPrice(long id);
        void DeleteCartProduct(long id);
        int GetTotalAmount(long id);
        void DeleteAllProductsFromCart(long userId);
    }
}
