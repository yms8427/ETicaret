using Microsoft.AspNetCore.Mvc;

namespace Yms.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Cart()
        {
            return View();
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