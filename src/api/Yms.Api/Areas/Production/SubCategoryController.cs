﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("list/{categoryId}")]
        [AllowAnonymous]
        public IEnumerable<SubCategoryDto> ListSubCategories(Guid categoryId)
        {
            return service.GetSubCategories(categoryId);
        }

        [HttpGet("category-detail")]
        [AllowAnonymous]
        public SubCategoryDto GetSubCategory([FromQuery] Guid id)
        {
            return service.GetSubCategory(id);
        }
    }
}