using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Api.Controllers
{
    public class PhotoUploadController : Controller
    {
        private IHostingEnvironment _env;

        public PhotoUploadController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            long size = 0;
            var filename = ContentDispositionHeaderValue
                .Parse(file.ContentDisposition)
                .FileName
                .Trim('"');

            var webRoot = _env.WebRootPath;
            var filePath = webRoot + "/Uploads" + $@"/{ filename }";

            bool fileExists = (System.IO.File.Exists(filePath) ? true : false);

            if (fileExists)
            {
                Random random = new Random();
                var randomNum = random.Next(99999);
                filename = randomNum + filename;
                filePath = webRoot + "/Uploads" + $@"/{ filename}";
            }
            size += file.Length;
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            return Ok(filename);

        }
    }
}