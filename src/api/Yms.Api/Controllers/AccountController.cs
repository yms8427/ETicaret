using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Yms.Api.Models;
using Yms.Common.Contracts;
using Yms.Services.Common.Abstractions;

namespace Yms.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService service;
        private readonly string signingKey;

        public AccountController(IUserService service, IOptions<Settings> options)
        {
            this.service = service;
            signingKey = options.Value.Authentication.Secret;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var user = service.Authenticate(model.UserName, model.Password);
            if (user != null)
            {
                var key = Encoding.ASCII.GetBytes(signingKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.GivenName, user.DisplayName)
                    }),
                    Expires = DateTime.Now.AddMinutes(50),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string jwtToken = tokenHandler.WriteToken(token);
                return Ok(jwtToken);
            }
            return BadRequest("incorrect credentials");
        }

        //[HttpPost("register")]
        //public IActionResult Register([FromBody] RegisterInputModel model)
        //{
        //    var dto = new RegisterDto
        //    {
        //        UserName = model.UserName,
        //        Password = model.Password,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        BirthDate = model.BirthDate
        //    };
        //    if (service.Register(dto))
        //    {
        //        return Ok(true);
        //    }
        //    return BadRequest(false);
        //}
    }
}
