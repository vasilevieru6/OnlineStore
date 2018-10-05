using OnlineShop.Services.ViewMoldels;
using OnlineShop.Models.Domain;
using System.Collections.Generic;

namespace OnlineShop.Services
{
    public interface ICartService
    {
        IList<CartItemViewModel> GetAllProducts(long id);
        void UpdateCartItemQuantity(QuantityCartItemViewModel cartItem);
        void AddProductToCart(QuantityCartItemViewModel cartItem, long id);
        PagedViewModel<CartItemViewModel> GetProducts(int pageNumber, int pageSize, long id);
        int GetCartProductPrice(long id);
        void DeleteCartProduct(long id);
    }
}
