using System;
using System.Collections.Generic;
using Yms.Contracts.Production;

namespace Yms.Services.Production.Abstractions
{
    public interface ISubCategoryService
    {
        Guid AddNewSubCategory(NewSubCategoryDto data);
        IEnumerable<SubCategoryDto> GetSubCategories(Guid categoryId);
        SubCategoryDto GetSubCategory(Guid subCategoryId);
    }
}
