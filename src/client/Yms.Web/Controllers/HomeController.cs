using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yms.Web.HttpHandlers;
using Yms.Web.Models;

namespace Yms.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IYmsApiHttpHandler httpHandler;

        public HomeController(IYmsApiHttpHandler httpHandler)
        {
            this.httpHandler = httpHandler;
        }

        public async Task<IActionResult> Index()
        {
            var tree = await httpHandler.GetCategoryTree();
            var suppliers = await httpHandler.GetSuppliers();
            var products = await httpHandler.GetProducts(10);//TODO : bunu kullan
            var vm = new MainViewModel
            {
                Products = HomePageProductViewModel.GetDummyProducts().ToList(),
                Categories = HomePageCategoryViewModel.FromHierachicalTemplate(tree).ToList(),
                Suppliers = HomePageSupplierViewModel.GetFromDto(suppliers).ToList(),
                Title = "ANA SAYFA"
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
