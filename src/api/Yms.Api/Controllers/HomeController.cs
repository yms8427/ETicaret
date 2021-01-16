using Microsoft.AspNetCore.Mvc;

namespace Yms.Api.Controllers //deneme yorumu
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
