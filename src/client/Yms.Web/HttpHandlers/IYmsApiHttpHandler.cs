using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yms.Contracts.Production;

namespace Yms.Web.HttpHandlers
{
    public interface IYmsApiHttpHandler
    {
        Task<CategoryHierarchyDto> GetCategoryTree();
        Task<List<ProductDto>> GetProducts(int limit);
        Task<List<ProductDto>> GetProductsByCategory(int limit, Guid id);
        Task<List<ProductDto>> GetProductsBySubCategory(int limit, Guid id);
        Task<List<ProductDto>> GetProductsBySupplier(int limit, Guid id);
        Task<List<SupplierDto>> GetSuppliers();
        Task<string> GetCategoryNameById(Guid categoryId);
        Task<string> GetSubCategoryNameById(Guid categoryId);
        Task<string> GetSupplierNameById(Guid supplierId);
    }
}
