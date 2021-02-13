using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yms.Common.Contracts;
using Yms.Contracts.Production;
using Yms.Services.OrderManagement.Abstractions;

namespace Yms.Api.Areas.Sales
{
    [Route("api/[area]/[controller]")]
    [Area("Sales")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService service;
        private readonly IClaims claims;

        public CartController(ICartService service, IClaims claims)
        {
            this.service = service;
            this.claims = claims;
        }

        [HttpPost("add/{productId}/{count}")]
        public IActionResult Add(Guid productId, byte count)
        {
            if (!claims.IsAuthenticated)
            {
                return Unauthorized();
            }
            var isSuccess = service.Add(claims.Session.Id, productId, count);
            if (isSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpGet("get-products/{userId}")]

        public IEnumerable<CartDto> GetProducts(Guid userId)
        {
            return service.GetProductByUserId(userId);
        }

        [HttpPost("update-cart")]

        public bool UpdateCart([FromQuery]Guid productId, [FromQuery]byte amount)
        {
            return service.UpdateCart(claims.Session.Id, productId, amount);
        }
    }
}
