using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OnlineShop.Models.Domain;
using OnlineShop.Repositories;
using OnlineShop.Services.ViewMoldels;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace OnlineShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private ICartService _cartService;

        public OrderService(IRepository repository, IMapper mapper, ICartService cartService)
        {
            _repository = repository;
            _mapper = mapper;
            _cartService = cartService;
        }

        private void CreateOrderLines(Order order)
        {
            var products = _cartService.GetAllProducts();
            foreach (var item in products)
            {
                var orderLine = new OrderLine()
                {
                    ProductId = item.ProductId,
                    Quatity = item.Quantity,
                    Price = item.UnitPrice,
                    Order = order
                };
                _repository.Add(orderLine);
            }
            _repository.Save();
        }

        public void Create(OrderViewModel orderViewModel, long userId)
        {
            var order = _mapper.Map<Order>(orderViewModel);
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.New;
            order.UserId = userId;
            order.TotalAmount = _cartService.GetTotalAmount(userId);
            CreateOrderLines(order);
            _cartService.DeleteAllProductsFromCart(userId);
        }

        public IList<OrderInfoViewModel> GetOrders(long userId)
        {
            var orders = _repository.GetAll<Order>().Where(x => x.UserId == userId);

            return orders
                .ProjectTo<OrderInfoViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
