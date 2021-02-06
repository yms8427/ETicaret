using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Yms.Services.Common.Abstractions;

namespace Yms.Api.Areas.Management
{
    [ApiController]
    [Area("Management")]
    [Route("api/[area]/[controller]")]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IDocumentService service;

        public ImageController(IDocumentService service)
        {
            this.service = service;
        }

        [HttpGet("get-name/{id}")]
        public string GetFileNameByDocumentId(Guid id)
        {
            return service.GetFileNameByDocumentId(id);
        }
    }
}

//https://www.yms.com/api/managament/image/upload
//https://www.yms.com/api/production/image/upload