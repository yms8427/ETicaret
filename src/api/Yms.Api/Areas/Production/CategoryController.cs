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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpPost("add-new")]
        public Guid AddCategory(NewCategoryDto data)
        {
            return service.AddNewCategory(data);
        }


        [HttpGet("list")]
        public IEnumerable<CategoryDto> ListCategories()
        {
            return service.GetCategories();
        }

        [HttpGet("category-detail")]
        public CategoryDto GetCategory([FromQuery] Guid id)
        {
            var category = service.GetCategory(id);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new InvalidOperationException("category not found");
            }
        }
    }
}
