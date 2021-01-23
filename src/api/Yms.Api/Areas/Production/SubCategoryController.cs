using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yms.Contracts.Production;
using Yms.Services.Production.Abstractions;

namespace Yms.Api.Areas.Production
{
    [Route("api/[area]/[controller]")]
    [Area("Production")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService service;

        public SubCategoryController(ISubCategoryService service)
        {
            this.service = service;
        }

        [HttpPost("add-new")]
        public Guid AddSubCategory(NewSubCategoryDto data)
        {
            return service.AddNewSubCategory(data);
        }


        [HttpGet("list")]
        public IEnumerable<SubCategoryDto> ListSubCategories()
        {
            return service.GetSubCategories();
        }

        [HttpGet("category-detail")]
        public SubCategoryDto GetSubCategory([FromQuery] Guid id)
        {
            var subCategory = service.GetSubCategory(id);
            if (subCategory != null)
            {
                return subCategory;
            }
            else
            {
                throw new InvalidOperationException("SubCategory not found");
            }
        }
    }
}
