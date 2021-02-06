using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Yms.Contracts.Production;
using Yms.Services.Production.Abstractions;

namespace Yms.Api.Areas.Production
{
    [Route("api/[area]/[controller]")]
    [Area("Production")]
    [ApiController]
    [Authorize]
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

//İsteği kim atmış şu an kimin isteği yürütülüyor , bunun için middleware
//register edilen sistem kullanıcıları için mail doğrulama
//Web sayfasının giriş işlemi

//Resim yükleme