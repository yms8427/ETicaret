using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Yms.Api.Areas.Management
{
    [ApiController]
    [Area("Management")]
    [Route("api/[area]/[controller]")]
    public class HomeController : ControllerBase
    {
    }
}


//https://www.yms.com/api/managament/image/upload
//https://www.yms.com/api/production/image/upload
