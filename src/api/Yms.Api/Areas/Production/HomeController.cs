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
        public IEnumerable<ProductDto> ListProducts([FromQuery] int count)
        {
            return service.GetProducts(count);
        }

        [HttpGet("product-detail")]
        public ProductDto GetProduct([FromQuery] Guid id)
        {
            return service.GetProduct(id);
        }
    }
}