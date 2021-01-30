using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yms.Contracts.Production;
using Yms.Services.Production.Abstractions;

namespace Yms.Api.Areas.Production
{
    [ApiController]
    [Area("Production")]
    [Route("api/[area]/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService service;

        public SupplierController(ISupplierService service)
        {
            this.service = service;
        }
        //[HttpPost("add-new")]
        //public Guid AddPoduct(NewProductDto data)
        //{
        //    return service.AddNewProduct(data);
        //}
        [HttpGet("list")]
        public IEnumerable<SupplierDto> ListSuppliers()
        {
            return service.GetSuppliers();
        }

    }
}
