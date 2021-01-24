using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yms.Contracts.Production;

namespace Yms.Services.Production.Helpers
{
    public static class CategoryComposer
    {
        public static CategoryHierarchyDto Compose(List<DetailedSubCategoryDto> data)
        {
            var result = new CategoryHierarchyDto();
            var categoryIds = data.Select(s => s.CategoryId).Distinct().ToList();
            categoryIds.ForEach(id =>
            {
                var category = data.First(f => f.CategoryId == id);
                var hierarchy = new Hierarchy
                {
                    Id = category.CategoryId,
                    Text = category.CategoryName
                };
                hierarchy.AddAttribute("Description", category.Description);

                var subCategories = data.Where(f => f.CategoryId == id).ToList();
                subCategories.ForEach(s =>
                {
                    var subHierarchy = new Hierarchy
                    {
                        Id = s.SubCategoryId,
                        Text = s.SubCategoryName
                    };
                    hierarchy.Items.Add(subHierarchy);
                });

                result.Items.Add(hierarchy);
            });

            return result;
        }
    }
}
