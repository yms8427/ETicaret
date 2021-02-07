using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Yms.Contracts.Production;
using Yms.Services.Production.Abstractions;

namespace Yms.Api.Areas.Production
{
    [ApiController]
    [Area("Production")]
    [Route("api/[area]/[controller]")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IProductService service;

        public HomeController(IProductService service)
        {
            this.service = service;
        }
        [HttpPost("add-new")]
        public Guid AddPoduct(NewProductDto data)
        {
            return service.AddNewProduct(data);
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public IEnumerable<ProductDto> ListProducts([FromQuery] int count)
        {
            return service.GetProducts(count);
        }

        [HttpGet("list-by-category")]
        [AllowAnonymous]
        public IEnumerable<ProductDto> GetProductByCategory([FromQuery] int count, [FromQuery] Guid id)
        {
            return service.GetProductsByCategory(count, id);
        }

        [HttpGet("list-by-subcategory")]
        [AllowAnonymous]
        public IEnumerable<ProductDto> GetProductBySubCategory([FromQuery] int count, [FromQuery] Guid id)
        {
            return service.GetProductsBySubCategory(count, id);
        }

        [HttpGet("list-by-supplier")]
        [AllowAnonymous]
        public IEnumerable<ProductDto> GetProductBySupplier([FromQuery] int count, [FromQuery] Guid id)
        {
            return service.GetProductsBySupplier(count, id);
        }

        [HttpGet("product-detail")]
        [AllowAnonymous]
        public ProductDto GetProduct([FromQuery] Guid id)
        {
            return service.GetProduct(id);
        }
    }
}