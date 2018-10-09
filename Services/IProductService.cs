using OnlineShop.Services.ViewMoldels;
using OnlineShop.Models.Domain;
using System.Collections.Generic;

namespace OnlineShop.Services
{
    public interface IProductService
    {
        IEnumerable<CategoryAndSubCategoryViewModel> GetProductCategoriesAndSubCategories();
        IList<ProductViewModel> GetInfoAboutProducts(string category, string subCategory);
        IList<ProductViewModel> GetAllProducts();
        void CreateProduct(ProductViewModel productViewModel);
        IEnumerable<CategoryViewModel> GetProductCategories(string category);
        IEnumerable<SubCategoryViewModel> GetProductSubCategories(string subCategory);
        PagedViewModel<ProductViewModel> GetProducts(int pageNumber, int pageSize);
        void DeleteProduct(long id);
        void UpdateProduct(ProductViewModel productViewModel);
        PagedViewModel<ProductViewModel> GetInfoAboutProductsOnPage(string category, string subCategory, int pageNumber, int pageSize);
        PagedViewModel<ProductViewModel> GetMostShippedProducts(int pageNumber, int pageSize);
    }
}
