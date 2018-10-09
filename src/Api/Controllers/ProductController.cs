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
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //GET api/values
        [HttpGet("categoriesAndSubCategories")]        
        public IEnumerable<CategoryAndSubCategoryViewModel> GetProductCategoriesAndSubCategories()
        {
            return _productService.GetProductCategoriesAndSubCategories();
        }

        [HttpGet("categories/{category}")]
        public IEnumerable<CategoryViewModel> GetProductCategories(string category)
        {
            return _productService.GetProductCategories(category);
        }

        [HttpGet("subCategories/{subCategory}")]
        public IEnumerable<SubCategoryViewModel> GetProductSubCategories(string subCategory)
        {
            return _productService.GetProductSubCategories(subCategory);
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
            _productService.UpdateProduct(productViewModel);
            return Json(productViewModel);
        }

        [HttpGet("mostShipped/{pageNumber}/{pageSize}")]
        public PagedViewModel<ProductViewModel> GetMostShipped(int pageNumber, int pageSize)
        {
            return _productService.GetMostShippedProducts(pageNumber, pageSize);
        }
        

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody]ProductViewModel productViewModel)
        {
            _productService.CreateProduct(productViewModel);
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
