using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Yms.Web.HttpHandlers;

namespace Yms.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IYmsApiHttpHandler httpHandler;
        private readonly IHostEnvironment environment;

        public ImageController(IYmsApiHttpHandler httpHandler, IHostEnvironment environment)
        {
            this.httpHandler = httpHandler;
            this.environment = environment;
        }
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return File(System.IO.File.ReadAllBytes(Path.Combine(environment.ContentRootPath, "wwwroot/img/nophoto.png")), "image/png");
            }
            var fileName = await httpHandler.GetDocumentNameById(id);
            return File(DownloadFile(fileName), "image/jpeg");
        }

        public static byte[] DownloadFile(string filePath)
        {
            var ftpServerUrl = string.Concat("ftp://localhost/img/", filePath);
            var request = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("SERGEN", "sk05345802402"); //TODO:  burayı anonim hale getirmen lazım
            //request.Credentials = new NetworkCredential(user, password);
            using (var response = (FtpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var memoryStream = new MemoryStream())
            {
                responseStream?.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}