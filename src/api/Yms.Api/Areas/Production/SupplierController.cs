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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService service;

        public SupplierController(ISupplierService service)
        {
            this.service = service;
        }

        [HttpGet("supplier/{supplierId}")]
        [AllowAnonymous]
        public string GetSupplierById(Guid supplierId)
        {
            return service.GetSupplierNameById(supplierId);
        }

        [HttpGet("supplier-detail/{id}")]
        [AllowAnonymous]
        public SupplierDto GetSupplier(Guid id)
        {
            return service.GetSupplier(id);
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public IEnumerable<SupplierDto> ListSuppliers()
        {
            return service.GetSuppliers();
        }

        [HttpPost("add-new")]
        public Guid AddSupplier(NewSupplierDto data)
        {
            return service.AddNewSupplier(data);
        }
    }
}