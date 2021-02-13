using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yms.Common.Contracts;
using Yms.Contracts.Production;
using Yms.Web.Models;

namespace Yms.Web.HttpHandlers
{
    public interface IYmsApiHttpHandler
    {
        Task<CategoryHierarchyDto> GetCategoryTree();
        Task<List<ProductDto>> GetProducts(int limit);
        Task<List<ProductDto>> GetProductsByCategory(int limit, Guid id);
        Task<List<ProductDto>> GetProductsBySubCategory(int limit, Guid id);
        Task<List<ProductDto>> GetProductsBySupplier(int limit, Guid id);
        Task<bool> AddToCart(Guid productId, byte count);
        Task<bool> Register(RegisterViewModel data);
        Task<CartMainViewModel> GetProductForCart(Guid userId);
        Task<List<SupplierDto>> GetSuppliers();
        Task<string> GetCategoryNameById(Guid categoryId);
        Task<DetailedSessionInformation> Authenticate(string userName, string password);
        Task<string> GetSubCategoryNameById(Guid categoryId);
        Task<string> GetSupplierNameById(Guid supplierId);
        Task<string> GetDocumentNameById(Guid id);
        Task<bool> UpdateCart(Guid productId, int amount);
    }
}
