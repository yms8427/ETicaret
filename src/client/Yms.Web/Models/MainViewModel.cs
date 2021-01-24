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
    }

    public class HomePageProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
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
