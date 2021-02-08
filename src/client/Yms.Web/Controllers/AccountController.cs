using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Yms.Web.HttpHandlers;
using Yms.Web.Models;

namespace Yms.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IYmsApiHttpHandler httpHandler;

        public AccountController(IYmsApiHttpHandler httpHandler)
        {
            this.httpHandler = httpHandler;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await httpHandler.Authenticate(model.UserName, model.Password);
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                new Claim(ClaimTypes.Name, result.UserName),
                new Claim(ClaimTypes.GivenName, result.DisplayName),
                new Claim("JwtToken", result.Token)
            };

            var claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false
            };

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                          new ClaimsPrincipal(claimsIdentity), 
                                          authProperties);
            return Redirect("/");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await httpHandler.Register(model);
            if (result)
            {
                return Redirect("/Account/Login");
            }
            else
            {
                return BadRequest("Kayıt Tamamlanamadı!");
            }
            
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
