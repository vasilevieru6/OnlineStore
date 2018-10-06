using OnlineShop.Services.ViewMoldels;
using OnlineShop.Models.Domain;
using OnlineShop.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;
using AutoMapper;

namespace OnlineShop.Services
{
    public class CartService : ICartService
    {
        private IRepository _repository;
        private readonly IMapper mapper;

        public CartService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public IList<CartItemViewModel> GetAllProducts()
        {
            IQueryable<ShoppingCartItem> shoppingCartItem = _repository.GetAll<ShoppingCartItem>();

            return shoppingCartItem
                .Select(x => new CartItemViewModel
                {
                    Id = x.Id,
                    Name = x.Product.Name,
                    UnitPrice = x.Product.UnitPrice,
                    Description = x.Product.Description,
                    Quantity = x.Quantity,
                    ShoppingCartId = x.ShoppingCartId,
                    ProductId = x.ProductId
                })
                .ToList();
        }

        public PagedViewModel<CartItemViewModel> GetProducts(int pageNumber, int pageSize, long userId)
        {
            IQueryable<ShoppingCartItem> queryable = _repository.GetAll<ShoppingCartItem>()
                        .Where(x => x.ShoppingCart.UserId == userId);

            var count = queryable.Count();

            var result = queryable
                .Select(x => new CartItemViewModel
                {
                    Id = x.Id,
                    Name = x.Product.Name,
                    UnitPrice = x.Product.UnitPrice,
                    Description = x.Product.Description,
                    Quantity = x.Quantity,
                    ShoppingCartId = x.ShoppingCartId,
                    ProductId = x.ProductId
                })
                .Paged(pageNumber, pageSize);

            return result;
        }

        public void UpdateCartItemQuantity(QuantityCartItemViewModel cartItem)
        {
            var item = _repository.GetById<ShoppingCartItem>(cartItem.Id);
            item.Quantity = cartItem.Quantity;
            _repository.Update(item);
            _repository.Save();
        }

        public int GetTotalAmount(long userId)
        {
            var shoppingCart = _repository.GetAll<ShoppingCart>()
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            return _repository.GetAll<ShoppingCartItem>()
                .Where(x => x.ShoppingCartId == shoppingCart.Id)
                .Sum(x => x.Quantity * x.Product.UnitPrice);
        }

        public int GetCartProductPrice(long userId)
        {
            return _repository.GetAll<ShoppingCartItem>()
                .Where(x => x.ShoppingCart.UserId == userId)
                .Select(x => x.Product.UnitPrice)
                .FirstOrDefault();
        }

        public void AddProductToCart(QuantityCartItemViewModel cartItem, long userId)
        {
            var currentUserCart = _repository.GetAll<ShoppingCart>()
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .FirstOrDefault();

            var item = _repository.GetAll<ShoppingCartItem>()
                .Where(x => x.ShoppingCartId == currentUserCart && x.ProductId == cartItem.ProductId)
                .FirstOrDefault();

            if (item != null)
            {
                item.Quantity++;
                _repository.Update(item);
                _repository.Save();
            }
            else
            {
                item = mapper.Map<ShoppingCartItem>(cartItem);
                item.ShoppingCartId = currentUserCart;
                _repository.Add(item);
                _repository.Save();
            }
        }

        public void DeleteCartProduct(long cartItemId)
        {
            var cartItem = _repository.GetById<ShoppingCartItem>(cartItemId);
            _repository.Delete(cartItem);
            _repository.Save();
        }


        public void DeleteAllProductsFromCart(long userId)
        {
            var cart = _repository.GetAll<ShoppingCart>()
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            var cartItems = _repository.GetAll<ShoppingCartItem>().Where(x => x.ShoppingCartId == cart.Id);

            foreach (var item in cartItems)
            {
                _repository.Delete(item);
            }
            _repository.Save();
        }
    }
}
