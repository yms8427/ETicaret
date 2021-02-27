using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Yms.Common.Caching.Abstractions;
using Yms.Contracts.Production;
using Yms.Data.Context;
using Yms.Data.Entities;
using Yms.Services.Production.Abstractions;
using Yms.Services.Production.Helpers;

namespace Yms.Services.Production.Concretes
{
    internal class CategoryService : ICategoryService
    {
        private readonly YmsDbContext context;
        private readonly ICacheManager cacheManager;

        public CategoryService(YmsDbContext context, ICacheManager cacheManager)
        {
            this.context = context;
            this.cacheManager = cacheManager;
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
                var dto = new CategoryDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    Name = c.Name
                };
                cacheManager.Update<List<CategoryDto>>(nameof(Category), (cachedData) => { cachedData.Add(dto); });
                return c.Id;
            }
            return Guid.Empty;

        }

        public IEnumerable<CategoryDto> GetCategories()
        {

            return cacheManager.GetOrCreate<List<CategoryDto>>(nameof(Category), () => FetchCategories());

        }

        private List<CategoryDto> FetchCategories()
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
            var categories = cacheManager.GetOrCreate<List<CategoryDto>>(nameof(Category), () => FetchCategories());
            if (categories != null)
            {
                return categories.FirstOrDefault(c => c.Id == categoryId);
            }
            else
            {
                throw new InvalidOperationException("category not found");
            }
        }

        public CategoryHierarchyDto GetCategoryHierarchy()
        {
            var subCategories = cacheManager.GetOrCreate<List<DetailedSubCategoryDto>>(nameof(DetailedSubCategoryDto), () => FetchDetailedSubCategories());

            return CategoryComposer.Compose(subCategories);
        }

        private List<DetailedSubCategoryDto> FetchDetailedSubCategories()
        {
            return context.SubCategories.Where(i => !i.IsDeleted)
                                               .Include(i => i.Category)
                                               .Select(s => new DetailedSubCategoryDto
                                               {
                                                   CategoryId = s.CategoryId,
                                                   CategoryName = s.Category.Name,
                                                   Description = s.Category.Description,
                                                   SubCategoryId = s.Id,
                                                   SubCategoryName = s.Name
                                               })
                                               .ToList();
        }

        public string GetCategoryNameById(Guid id)
        {
            //var cat = context.Categories.FirstOrDefault(f => f.Id == id);
            //if (cat != null)
            //{
            //    return cat.Name;
            //}
            //return string.Empty;
            var categories = cacheManager.GetOrCreate<List<CategoryDto>>(nameof(Category), () => FetchCategories());

            return categories.FirstOrDefault(f => f.Id == id)?.Name;
        }

        public string GetSubCategoryNameById(Guid id)
        {
            var subCategories = cacheManager.GetOrCreate<List<DetailedSubCategoryDto>>(nameof(DetailedSubCategoryDto), () => FetchDetailedSubCategories());

            return subCategories.FirstOrDefault(f => f.SubCategoryId == id)?.SubCategoryName;
        }
    }
}
