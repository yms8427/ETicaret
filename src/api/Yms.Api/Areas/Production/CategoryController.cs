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
            return service.GetCategory(id);
        }

        [HttpGet("get-category-tree")]
        public CategoryHierarchyDto GetCategoryHierarchy()
        {
            return service.GetCategoryHierarchy();
        }

        [HttpGet("category/{categoryId}")]
        public string GetCategoryById(Guid categoryId)
        {
            return service.GetCategoryNameById(categoryId);
        }

        [HttpGet("sub-category/{categoryId}")]
        public string GetSubCategoryById(Guid categoryId)
        {
            return service.GetSubCategoryNameById(categoryId);
        }
    }
}
