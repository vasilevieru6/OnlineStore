using OnlineShop.Services.ViewMoldels;
using OnlineShop.Models.Domain;
using OnlineShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Services
{
    public class ProductService : IProductService
    {
        private IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CategoryViewModel> GetProductCategories(string category)
        {
            var categories = _repository.GetAll<Product>();
            return categories.Select(x => new CategoryViewModel
            {
                Category = x.Category
            })
            .Where(x=> x.Category.Contains(category))
            .Distinct()
            .ToList()
            .ToArray();
        }

        public IEnumerable<SubCategoryViewModel> GetProductSubCategories(string subCategory)
        {
            var subCategories = _repository.GetAll<Product>();

            return subCategories.Select(x => new SubCategoryViewModel
            {
                SubCategory = x.SubCategory
            })
            .Where(x => x.SubCategory.Contains(subCategory))
            .Distinct()
            .ToList()
            .ToArray();

        }
        public IEnumerable<CategoryAndSubCategoryViewModel> GetProductCategoriesAndSubCategories()
        {
            var products = _repository.GetAll<Product>();
            var allCategories = products.Select(x => new { x.Category, x.SubCategory })
                .Distinct()
                .ToList();

            return allCategories
                .GroupBy(x => x.Category)
                .Select(x => new CategoryAndSubCategoryViewModel
                {
                    Category = x.Key,
                    SubCategories = x.Select(y => y.SubCategory).ToArray()
                })
                .ToArray();          
        }

        public IList<ProductViewModel> GetInfoAboutProducts(string category, string subCategory)
        {
            var products = _repository.GetAll<Product>();

          
            return products
                .Select(x => new ProductViewModel
                {       
                    Id = x.Id,
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    Description = x.Description,
                    PhotoUrl = x.PhotoUrl,
                    Category = x.Category,
                    SubCategory = x.SubCategory
                }).Where(x => x.Category == category && x.SubCategory == subCategory).ToList();
        }

        public PagedViewModel<ProductViewModel> GetInfoAboutProductsOnPage(string category, string subCategory, int pageNumber, int pageSize)
        {
            var products = _repository.GetAll<Product>();


            return products
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    Description = x.Description,
                    PhotoUrl = x.PhotoUrl,
                    Category = x.Category,
                    SubCategory = x.SubCategory
                })
                .Where(x => x.Category == category && x.SubCategory == subCategory)
                .Paged(pageNumber, pageSize);                
        }

        public IList<ProductViewModel> GetAllProducts()
        {
            var products = _repository.GetAll<Product>();

            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                Description = x.Description,
                PhotoUrl = x.PhotoUrl,
                Category = x.Category,
                SubCategory = x.SubCategory
            }).ToList();
        }

        public void CreateProduct(Product product)
        {
            _repository.Add(product);
            _repository.Save();
        }
        
        public PagedViewModel<ProductViewModel> GetProducts(int pageNumber, int pageSize)
        {
            IQueryable<Product> queryable = _repository.GetAll<Product>();

            var count = queryable.Count();

            var result = queryable
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    Description = x.Description,
                    Category = x.Category,
                    SubCategory = x.SubCategory,
                    PhotoUrl = x.PhotoUrl
                })
                .Paged(pageNumber, pageSize);

            return result;
        }

        public void DeleteProduct(long id)
        {
            var product = _repository.GetById<Product>(id);
            _repository.Delete(product);
            _repository.Save();
        }

        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
            _repository.Save();
        }
    }
}
