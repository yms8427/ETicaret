using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Yms.Contracts.Production;
using Yms.Services.Common.Abstractions;
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
        private readonly IDocumentService documentService;

        public HomeController(IProductService service, IDocumentService documentService)
        {
            this.service = service;
            this.documentService = documentService;
        }
        [HttpPost("add-new")]
        public Guid AddProduct(NewProductDto data)
        {
            return service.AddNewProduct(data);
        }

        [HttpPost("upload")]
        public bool Upload([FromForm]UploadFileDto data)
        {
            var id = documentService.UploadFile(data.Content.OpenReadStream(), data.Content.FileName);
            return service.SetMainImage(data.ProductId, id);
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