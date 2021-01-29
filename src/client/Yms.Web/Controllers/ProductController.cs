using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yms.Web.HttpHandlers;
using Yms.Web.Models;

namespace Yms.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IYmsApiHttpHandler httpHandler;

        public ProductController(IYmsApiHttpHandler httpHandler)
        {
            this.httpHandler = httpHandler;
        }

        public async Task<IActionResult> Category(string Id)
        {

            var categoryId = Guid.Parse(Id);


            var tree = await httpHandler.GetCategoryTree();
            var suppliers = await httpHandler.GetSuppliers();
            var products = await httpHandler.GetProductsByCategory(10, categoryId);
            var vm = new MainViewModel()
            {
                Categories = HomePageCategoryViewModel.FromHierachicalTemplate(tree).ToList(),
                Suppliers = HomePageSupplierViewModel.GetFromDto(suppliers).ToList(),
                Products = HomePageProductViewModel.GetFromDto(products).ToList()
            };

            return View(vm);
        }
        public async Task<IActionResult> SubCategory(string Id)
        {

            var categoryId = Guid.Parse(Id);


            var tree = await httpHandler.GetCategoryTree();
            var suppliers = await httpHandler.GetSuppliers();
            var products = await httpHandler.GetProductsBySubCategory(10, categoryId);
            var vm = new MainViewModel()
            {
                Categories = HomePageCategoryViewModel.FromHierachicalTemplate(tree).ToList(),
                Suppliers = HomePageSupplierViewModel.GetFromDto(suppliers).ToList(),
                Products = HomePageProductViewModel.GetFromDto(products).ToList()
            };

            return View(vm);
        }
        public async Task<IActionResult> Supplier(string Id)
        {

            var categoryId = Guid.Parse(Id);


            var tree = await httpHandler.GetCategoryTree();
            var suppliers = await httpHandler.GetSuppliers();
            var products = await httpHandler.GetProductsBySupplier(10, categoryId);
            var vm = new MainViewModel()
            {
                Categories = HomePageCategoryViewModel.FromHierachicalTemplate(tree).ToList(),
                Suppliers = HomePageSupplierViewModel.GetFromDto(suppliers).ToList(),
                Products = HomePageProductViewModel.GetFromDto(products).ToList()
            };

            return View(vm);
        }



    }
}
