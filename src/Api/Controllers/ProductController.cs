using OnlineShop.Services;
using OnlineShop.Services.ViewMoldels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.Domain;
using System.Collections.Generic;

namespace OnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private IProductService _productService;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        //GET api/values
        [HttpGet("categoriesAndSubCategories")]        
        public IEnumerable<CategoryAndSubCategoryViewModel> GetProductCategoriesAndSubCategories()
        {
            IEnumerable < CategoryAndSubCategoryViewModel > categories = _productService.GetProductCategoriesAndSubCategories();
            return categories;
        }

        [HttpGet("categories/{category}")]
        public IEnumerable<CategoryViewModel> GetProductCategories(string category)
        {
            IEnumerable<CategoryViewModel> categories = _productService.GetProductCategories(category);
            return categories;
        }

        [HttpGet("subCategories/{subCategory}")]
        public IEnumerable<SubCategoryViewModel> GetProductSubCategories(string subCategory)
        {
            IEnumerable<SubCategoryViewModel> subCategories = _productService.GetProductSubCategories(subCategory);
            return subCategories;
        }

        [HttpGet("{category}/{subCategory}")]
        public IList<ProductViewModel> GetProducts(string category, string subCategory)
        {
            return _productService.GetInfoAboutProducts(category, subCategory);
        }

        [HttpGet("{category}/{subCategory}/{pageNumber}/{pageSize}")]
        public PagedViewModel<ProductViewModel> GetProductsInfoPaged(string category, string subCategory, int pageNumber, int pageSize)
        {
            return _productService.GetInfoAboutProductsOnPage(category, subCategory, pageNumber, pageSize);
        }


        [HttpGet("items")]
        [Authorize(Roles = "Admin")]
        public IList<ProductViewModel> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        [HttpGet("items/{pageNumber}/{pageSize}")]
        public PagedViewModel<ProductViewModel> GetProducts(int pageNumber, int pageSize)
        {
            return _productService.GetProducts(pageNumber, pageSize);
        }

        [HttpPatch("{id}")]
        public IActionResult PartialUpdateProduct([FromBody] ProductViewModel productViewModel, long id)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);
            _productService.UpdateProduct(product);

            return Json(product);
        }

        //POST api/values
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody]ProductViewModel productViewModel)
        {
            var product = _mapper.Map<ProductViewModel, Product>(productViewModel);
            _productService.CreateProduct(product);
            return Json(productViewModel);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
    }
}
