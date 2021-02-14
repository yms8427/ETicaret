using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yms.Common.Contracts;
using Yms.Web.HttpHandlers;
using Yms.Web.Models;

namespace Yms.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IYmsApiHttpHandler httpHandler;
        private readonly IClaims claims;

        public OrderController(IYmsApiHttpHandler httpHandler, IClaims claims)
        {
            this.httpHandler = httpHandler;
            this.claims = claims;
        }

        public IActionResult Cart()
        {
            if (!claims.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var data = await httpHandler.GetProductForCart(claims.Session.Id);
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartViewModel model)
        {
            var result = await httpHandler.UpdateCart(model.ProductId, model.Amount);
            return Json(result);
        }

        public async Task<IActionResult> AddToCart(Guid productId)
        {
            var result = await httpHandler.AddToCart(productId, 1);
            if (result)
            {
                return Redirect($"/Order/Cart");
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