using System;
using System.Collections.Generic;
using System.Text;
using Yms.Contracts.Production;

namespace Yms.Services.Production.Abstractions
{
    public interface IProductService
    {
        Guid AddNewProduct(NewProductDto data);
        IEnumerable<ProductDto> GetProducts(int count);
        ProductDto GetProduct(Guid productId);
        IEnumerable<ProductDto> GetProductsByCategory(int count, Guid id);
        IEnumerable<ProductDto> GetProductsBySubCategory(int count, Guid id);
        IEnumerable<ProductDto> GetProductsBySupplier(int count, Guid id);
    }
}
