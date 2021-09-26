using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFileAspNetCore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Path.GetFileName(formFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size });
        }

    }
}
