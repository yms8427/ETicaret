using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yms.Contracts.Production;
using Yms.Data.Entities;
using Yms.Services.Production.Abstractions;

namespace Yms.Services.Production.Concretes
{
    //UnitOfWork Design Pattern
    //Repository Design Pattern
    //Generic Repository DesignPattern
    internal class ProductService : IProductService
    {
        private readonly DbSet<Product> products;
        private readonly DbSet<SubCategory> subCategories;
        private readonly DbSet<Supplier> suppliers;
        private readonly DbContext context;

        public ProductService(DbContext context)
        {
            products = context.Set<Product>();
            suppliers = context.Set<Supplier>();
            subCategories = context.Set<SubCategory>();
            this.context = context;
        }
        public Guid AddNewProduct(NewProductDto data)
        {
            var hasSupplier = suppliers.Any(a => a.Id == data.SupplierId);
            if (!hasSupplier)
            {
                throw new InvalidOperationException("Geçersiz ürün sağlayıcısı");
            }

            var hasCategory = subCategories.Any(a => a.Id == data.SubCategoryId);
            if (!hasCategory)
            {
                throw new InvalidOperationException("Geçersiz ürün kategorisi");
            }
            var p = new Product
            {
                Id = Guid.NewGuid(),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                UpdatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsActive = true,
                IsDeleted = false,
                Name = data.Name,
                Price = data.Price,
                Stock = data.Stock,
                SubCategoryId = data.SubCategoryId,
                SupplierId = data.SupplierId
            };
            products.Add(p);

            var savedItemCount = context.SaveChanges();
            if (savedItemCount > 0)
            {
                return p.Id;
            }
            return Guid.Empty;
        }

        public IEnumerable<ProductDto> GetProducts(int count)
        {
            return products.Include(i => i.SubCategory).Where(p => p.IsActive).Take(count).Select(s => new ProductDto
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                Stock = s.Stock,
                Category = s.SubCategory.Name
            }).ToList();
        }

        public ProductDto GetProduct(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
