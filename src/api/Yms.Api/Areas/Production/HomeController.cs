using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Yms.Contracts.Production;

namespace Yms.Api.Areas.Production
{
    [ApiController]
    [Area("Production")]
    [Route("api/[area]/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost("add-new")]
        public Guid AddPoduct(NewProductDto data)
        {
            return Guid.NewGuid();
        }

        [HttpGet("list")]
        public IEnumerable<ProductDto> ListProducts([FromQuery]int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new ProductDto { Name = "Ürün " + i, Price = 3 * (i + 2) };
            }
        }
    }
}