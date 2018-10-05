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


        public IList<CartItemViewModel> GetAllProducts(long id)
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

        public PagedViewModel<CartItemViewModel> GetProducts(int pageNumber, int pageSize, long id)
        {
            IQueryable<ShoppingCartItem> queryable = _repository.GetAll<ShoppingCartItem>()
                        .Where(x => x.ShoppingCart.UserId == id);

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
        }

        public int GetCartProductPrice(long id)
        {
            return _repository.GetAll<ShoppingCartItem>().Where(x => x.ShoppingCart.UserId == id).Select(x => x.Product.UnitPrice).FirstOrDefault();
        }

        public void AddProductToCart(QuantityCartItemViewModel cartItem, long id)
        {
            var currentUserCart = _repository.GetAll<ShoppingCart>()
                .Where(x => x.UserId == id)
                .Select(x => x.Id)
                .FirstOrDefault();

            var item = _repository.GetAll<ShoppingCartItem>()
                .Where(x => x.ShoppingCartId == currentUserCart && x.ProductId == cartItem.ProductId)
                .FirstOrDefault();

            if (item != null)
            {
                item.Quantity++;
                _repository.Update(item);
            }
            else
            {
                item = mapper.Map<ShoppingCartItem>(cartItem);
                item.ShoppingCartId = currentUserCart;
                _repository.Add(item);
            }

        }

        public void DeleteCartProduct(long id)
        {
            var cartItem = _repository.GetById<ShoppingCartItem>(id);
            _repository.Delete(cartItem);
        }
    }
}
