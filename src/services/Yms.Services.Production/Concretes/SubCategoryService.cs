using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Yms.Common.Caching.Abstractions;
using Yms.Common.Contracts;
using Yms.Contracts.Production;
using Yms.Data.Context;
using Yms.Data.Entities;
using Yms.Services.Production.Abstractions;

namespace Yms.Services.Production.Concretes
{
    internal class SubCategoryService : ISubCategoryService
    {
        private readonly YmsDbContext context;
        private readonly IClaims claims;
        private readonly ICacheManager cacheManager;

        public SubCategoryService(YmsDbContext context, IClaims claims, ICacheManager cacheManager)
        {
            this.context = context;
            this.claims = claims;
            this.cacheManager = cacheManager;
        }
        public Guid AddNewSubCategory(NewSubCategoryDto data)
        {
            var categories = cacheManager.GetOrCreate<List<CategoryDto>>(nameof(Category), () => FetchCategories());
            var hasCategory = categories.Any(f => f.Id == data.CategoryId);
            if (!hasCategory)
            {
                throw new InvalidOperationException("Category not found");
            }
            var sc = new SubCategory
            {
                Id = Guid.NewGuid(),
                CategoryId = data.CategoryId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                ImageUrl = null,
                Name = data.Name,
                CreatedBy = claims.Session.Id,
                UpdatedBy = claims.Session.Id,
            };
            context.SubCategories.Add(sc);
            var resultValue = context.SaveChanges();

            if (resultValue > 0)
            {
                var dto = new SubCategoryDto
                {
                    Id = sc.Id,
                    ImgUrl = sc.ImageUrl,
                    Name = sc.Name
                };
                cacheManager.Update<List<SubCategoryDto>>(nameof(SubCategory), (cachedData) =>
                {
                    cachedData.Add(dto);
                });
                return sc.Id;
            }
            return Guid.Empty;
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


        public IEnumerable<SubCategoryDto> GetSubCategories(Guid categoryId)
        {
            return cacheManager.GetOrCreate<List<SubCategoryDto>>(categoryId.ToString(), () => FetchSubCategoriesById(categoryId));
        }

        private List<SubCategoryDto> FetchSubCategoriesById(Guid categoryId)
        {
            return context.SubCategories.Where(f => f.CategoryId == categoryId).Select(sc => new SubCategoryDto
            {
                Id = sc.Id,
                Name = sc.Name,
                ImgUrl = sc.ImageUrl

            }).ToList();
        }
        private List<SubCategoryDto> FetchSubCategories()
        {
            return context.SubCategories.Select(sc => new SubCategoryDto
            {
                Id = sc.Id,
                Name = sc.Name,
                ImgUrl = sc.ImageUrl

            }).ToList();
        }

        public SubCategoryDto GetSubCategory(Guid subCategoryId)
        {
            var subCategories = cacheManager.GetOrCreate<List<SubCategoryDto>>(nameof(SubCategory), () => FetchSubCategories());
            if (subCategories != null)
            {
                return subCategories.FirstOrDefault(s=>s.Id == subCategoryId);
            }
            else
            {
                throw new InvalidOperationException("SubCategory not found");
            }
        }
    }
}
