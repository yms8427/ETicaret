using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yms.Common.Contracts;
using Yms.Web.HttpHandlers;

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

        public async Task<IActionResult> Cart()
        {
            if (!claims.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            var data = await httpHandler.GetProductForCart(claims.Session.Id);
            return View(data);
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