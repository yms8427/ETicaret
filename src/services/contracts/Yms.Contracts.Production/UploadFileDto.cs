using Microsoft.AspNetCore.Http;
using System;

namespace Yms.Contracts.Production
{
    public class UploadFileDto
    {
        public Guid ProductId { get; set; }
        public IFormFile Content { get; set; }
    }
}
