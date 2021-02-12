using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yms.Web.HttpHandlers;

namespace Yms.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IYmsApiHttpHandler httpHandler;
        private readonly Guid userId;

        public OrderController(IYmsApiHttpHandler httpHandler, IHttpContextAccessor httpContextAccessor)
        {
            this.httpHandler = httpHandler;
            try
            {
                userId = Guid.Parse(httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch
            {
            }
        }

        public async Task<IActionResult> Cart()
        {
            var data = await httpHandler.GetProductForCart(userId);
            if (data == null)
            {
                return Redirect("/Account/Login");
            }
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