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
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<QuantityCartItemViewModel, ShoppingCartItem>();
            CreateMap<AddressViewModel, Address>().ReverseMap();
            CreateMap<OrderViewModel, Order>();
            
        }
    }
}
