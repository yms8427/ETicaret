using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yms.Web.HttpHandlers;

namespace Yms.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IYmsApiHttpHandler httpHandler;
        public OrderController(IYmsApiHttpHandler httpHandler)
        {
            this.httpHandler = httpHandler;
        }
        public IActionResult Cart()
        {
            return View();
        }

        public async Task<IActionResult> AddToCart(Guid productId)
        {
            var result = await httpHandler.AddToCart(productId, 1);
            if (result)
            {
                return Redirect("/Order/Cart");
            }
            return Redirect("/Account/Login");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Invoice()
        {
            return View();
        }
    }
}