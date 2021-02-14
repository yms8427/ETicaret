using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Yms.Api.Models;
using Yms.Common.Contracts;
using Yms.Contracts.CommonServices;
using Yms.Services.Common.Abstractions;

namespace Yms.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IMailService mailService;
        private readonly string signingKey;

        public AccountController(IUserService service, IMailService mailService, IOptions<Settings> options)
        {
            this.service = service;
            this.mailService = mailService;
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
                var result = new DetailedSessionInformation
                {
                    Token = tokenHandler.WriteToken(token),
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    UserName = user.UserName
                };
                return Ok(result);
            }
            return BadRequest("incorrect credentials");
        }

        [HttpGet("list")]
        public IEnumerable<UserDto> ListUsers()
        {
            return service.GetUsers();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] NewUserDto newUser)
        {
            var (saved, userId, verificationCode) = service.Register(newUser);
            if (saved)
            {
                var content = new StringBuilder("<html><body>");
                content.Append("<h1>Hoşgeldiniz. Üyeliğiniz onaylandı.</h1>");
                content.Append($"<p>Üyeliğinizin aktifleştirilmesi ve parolanızı belirlemek için <a href=\"https://localhost:6001/account/verify?code={verificationCode}\">buraya</a> tıklayınız</p>");
                content.Append("</body></html>");
                mailService.SendVerificationMail(userId, content.ToString());
                return Ok(true);
            }
            return BadRequest(false);
        }
    }
}