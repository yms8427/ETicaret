using System;
using System.Collections.Generic;
using System.Linq;
using Yms.Contracts.Production;
using Yms.Data.Context;
using Yms.Data.Entities;
using Yms.Services.Production.Abstractions;

namespace Yms.Services.Production.Concretes
{
    internal class CategoryService : ICategoryService
    {
        private readonly YmsDbContext context;

        public CategoryService(YmsDbContext context)
        {
            this.context = context;
        }

        public Guid AddNewCategory(NewCategoryDto data)
        {
            var c = new Category
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Description = data.Description,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001")

            };
            context.Categories.Add(c);

            var resultValue = context.SaveChanges();
            if (resultValue > 0)
            {
                return c.Id;
            }
            return Guid.Empty;

        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            return context.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public CategoryDto GetCategory(Guid categoryId)
        {
            return context.Categories.Where(c => c.Id == categoryId).Select(c => new CategoryDto
            {
                Id = c.Id,
                Description = c.Description,
                Name = c.Name
            }).FirstOrDefault();
        }
    }
}
