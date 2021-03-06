﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public OrderController(IYmsApiHttpHandler httpHandler, IClaims claims, IConfiguration configuration)
        {
            this.httpHandler = httpHandler;
            this.claims = claims;
            this.configuration = configuration;
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

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            var result = await httpHandler.RemoveFromCart(id);
            return Json(result);
        }

        public async Task<IActionResult> AddToCart(AddToCartModel model)
        {
            var result = await httpHandler.AddToCart(model.ProductId, model.Count);
            if (result)
            {
                var connection = new HubConnectionBuilder()
                                     .WithUrl(configuration.GetValue<string>("WSUrl"))
                                     .Build();
                await connection.StartAsync();
                await connection.InvokeAsync("CartInsertion", claims.Session.UserName, model.ProductId);
                return Redirect($"/Order/Cart");
            }
            return Redirect("/Account/Login");
        }


        public async Task<IActionResult> GetCartAmount(Guid id)
        {
            var result = await httpHandler.GetCartAmount(id);
            return Json(result);
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