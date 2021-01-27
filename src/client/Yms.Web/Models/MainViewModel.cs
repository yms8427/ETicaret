using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yms.Contracts.Production;

namespace Yms.Web.Models
{
    public class MainViewModel
    {
        public List<HomePageProductViewModel> Products { get; set; }
        public List<HomePageCategoryViewModel> Categories { get; set; }
        public List<HomePageSupplierViewModel> Suppliers { get; set; }
    }

    public class HomePageSupplierViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static IEnumerable<HomePageSupplierViewModel> GetFromDto(List<SupplierDto> suppliers)
        {
            return suppliers.Select(s => new HomePageSupplierViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
    }

    public class HomePageProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public static IEnumerable<HomePageProductViewModel> GetDummyProducts()
        {
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Kui Ye Chen’s AirPods", Price = 250, ImageUrl = "img/product-1.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Air Jordan 12 gym red", Price = 300, ImageUrl = "img/product-2.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Cyan cotton t-shirt", Price = 25, ImageUrl = "img/product-3.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Timex Unisex Originals", Price = 188, ImageUrl = "img/product-4.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Red digital smartwatch", Price = 320, ImageUrl = "img/product-5.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Nike air max 95", Price = 139, ImageUrl = "img/product-6.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Joemalone Women prefume", Price = 175, ImageUrl = "img/product-7.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Apple Watch", Price = 382, ImageUrl = "img/product-8.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Men silver Byron Watch", Price = 250, ImageUrl = "img/product-9.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Ploaroid one step camera", Price = 250, ImageUrl = "img/product-10.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Gray Nike running shoes", Price = 340, ImageUrl = "img/product-11.jpg" };
            yield return new HomePageProductViewModel { Id = Guid.NewGuid(), Name = "Black DSLR lense", Price = 351, ImageUrl = "img/product-12.jpg" };
        }

        public static IEnumerable<HomePageProductViewModel> GetFromDto(List<ProductDto> products)
        {
            return products.Select(p => new HomePageProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            });
        }
    }

    public class HomePageCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SubCategoryViewModel> SubItems { get; set; }

        public class SubCategoryViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public static IEnumerable<HomePageCategoryViewModel> FromHierachicalTemplate(CategoryHierarchyDto data)
        {
            foreach (var category in data.Items)
            {
                var vm = new HomePageCategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Text,
                    Description = category.Attributes["Description"]
                };
                if (category.Items.Any())
                {
                    vm.SubItems = category.Items.Select(s => new SubCategoryViewModel
                    {
                        Id = s.Id,
                        Name = s.Text
                    }).ToList();
                }
                yield return vm;
            }
        }
    }
}
