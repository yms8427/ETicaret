using System;
using System.Collections.Generic;
using Yms.Contracts.Production;

namespace Yms.Services.Production.Abstractions
{
    public interface ICategoryService
    {
        Guid AddNewCategory(NewCategoryDto data);
        IEnumerable<CategoryDto> GetCategories();
        CategoryDto GetCategory(Guid categoryId);
        CategoryHierarchyDto GetCategoryHierarchy();
        string GetCategoryNameById(Guid id);
        string GetSubCategoryNameById(Guid id);
    }
}
