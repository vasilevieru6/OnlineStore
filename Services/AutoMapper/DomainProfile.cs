using OnlineShop.Services.ViewMoldels;
using AutoMapper;
using OnlineShop.Models.Domain;

namespace OnlineShop.Services.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Product, CategoryAndSubCategoryViewModel>();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<QuantityCartItemViewModel, ShoppingCartItem>();
            CreateMap<AddressViewModel, Address>().ReverseMap();
            CreateMap<OrderViewModel, Order>();
            CreateMap<ShoppingCartItem, CartItemViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Product.Name))
                .ForMember(x => x.UnitPrice, y => y.MapFrom(x => x.Product.UnitPrice))
                .ForMember(x => x.Description, y => y.MapFrom(x => x.Product.Description))
                .ForMember(x => x.Quantity, y => y.MapFrom(x => x.Quantity))
                .ForMember(x => x.ShoppingCartId, y => y.MapFrom(x => x.ShoppingCartId))
                .ForMember(x => x.ProductId, y => y.MapFrom(x => x.ProductId));

            CreateMap<Order, OrderInfoViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.OrderDate, y => y.MapFrom(x => x.OrderDate))
                .ForMember(x => x.TotalAmount, y => y.MapFrom(x => x.TotalAmount))
                .ForMember(x => x.Address, y => y.MapFrom(x => x.Address));
        }
    }
}
