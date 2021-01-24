using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Yms.Contracts.Production;
using Yms.Data.Context;
using Yms.Data.Entities;
using Yms.Services.Production.Abstractions;

namespace Yms.Services.Production.Concretes
{
    internal class SubCategoryService : ISubCategoryService
    {
        private readonly YmsDbContext context;

        public SubCategoryService(YmsDbContext context)
        {
            this.context = context;
        }
        public Guid AddNewSubCategory(NewSubCategoryDto data)
        {
            var hasCategory = context.Categories.Any(f => f.Id == data.CategoryId);
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
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            };
            context.SubCategories.Add(sc);
            var resultValue = context.SaveChanges();

            if (resultValue > 0)
            {
                return sc.Id;
            }
            return Guid.Empty;
        }

        public IEnumerable<SubCategoryDto> GetSubCategories(Guid categoryId)
        {
            return context.SubCategories.Where(f => f.CategoryId == categoryId).Select(sc => new SubCategoryDto
            {
                Id = sc.Id,
                Name = sc.Name,
                ImgUrl = sc.ImageUrl

            }).ToList();
        }

        public SubCategoryDto GetSubCategory(Guid subCategoryId)
        {
            var subCategory = context.SubCategories.Where(sc => sc.Id == subCategoryId)
                                                   .Select(s => new SubCategoryDto
                                                   {
                                                       Id = s.Id,
                                                       ImgUrl = s.ImageUrl,
                                                       Name = s.Name,
                                                   }).FirstOrDefault();
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
