using Microsoft.AspNetCore.Mvc;

namespace Yms.Api.Controllers //Deneme Yorumu
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController :  ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hi!");
        }

    }
}
