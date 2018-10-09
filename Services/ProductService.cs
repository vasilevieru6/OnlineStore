using OnlineShop.Services.ViewMoldels;
using OnlineShop.Models.Domain;
using OnlineShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace OnlineShop.Services
{
    public class ProductService : IProductService
    {
        private IRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<CategoryViewModel> GetProductCategories(string category)
        {
            var categories = _repository.GetAll<Product>();
            return categories.Select(x => new CategoryViewModel
            {
                Category = x.Category
            })
            .Where(x => x.Category.Contains(category))
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
                .Where(x => x.Category == category && x.SubCategory == subCategory)
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public PagedViewModel<ProductViewModel> GetInfoAboutProductsOnPage(string category, string subCategory, int pageNumber, int pageSize)
        {
            var products = _repository.GetAll<Product>();

            return products
                .Where(x => x.Category == category && x.SubCategory == subCategory)
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .Paged(pageNumber, pageSize);
        }

        public IList<ProductViewModel> GetAllProducts()
        {
            var products = _repository.GetAll<Product>();

            return products
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)                
                .ToList();
        }

        public void CreateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);
            _repository.Add(product);
            _repository.Save();
        }

        public PagedViewModel<ProductViewModel> GetProducts(int pageNumber, int pageSize)
        {
            IQueryable<Product> queryable = _repository.GetAll<Product>();

            var count = queryable.Count();

            var result = queryable
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .Paged(pageNumber, pageSize);

            return result;
        }

        public void DeleteProduct(long id)
        {
            var product = _repository.GetById<Product>(id);
            _repository.Delete(product);
            _repository.Save();
        }

        public void UpdateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);
            _repository.Update(product);
            _repository.Save();
        }

        public PagedViewModel<ProductViewModel> GetMostShippedProducts(int pageNumber, int pageSize)
        {            
            var productIds = _repository.GetAll<OrderLine>()
                .GroupBy(x => x.ProductId)
                .Select(x => new { count = x.Sum(y => y.Quatity), id = x.Key })
                .OrderByDescending(x => x.count)
                .Select(x => x.id)
                .Take(10);

            IQueryable<Product> products = _repository.GetAll<Product>()
                .Where(x => productIds.Contains(x.Id));

            var count = products.Count();

            var result = products
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .Paged(pageNumber, pageSize);

            return result;
        }
    }
}
