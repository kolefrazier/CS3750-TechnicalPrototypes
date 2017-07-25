using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using CS3750TechnicalPrototypes.Data;
using CS3750TechnicalPrototypes.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CS3750TechnicalPrototypes.Models;
using CS3750TechnicalPrototypes.Data;

namespace CS3750TechnicalPrototypes.Controllers
{
    public class UploadFilesController : Controller
    {
        private readonly AuctionContext _context;
        private IHostingEnvironment hostingEnv;


        public UploadFilesController(AuctionContext context, IHostingEnvironment env)
        {
            _context = context;
            hostingEnv = env;
        }

        public IActionResult UploadView()
        {
            var pics = _context.Media.ToList();
            return View(pics);
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            //var filePath = "";
            var fName = "";
            var test = "";

            foreach (var formFile in files)
            {
                fName = Path.GetFileName(formFile.FileName);
                //if not file name foreach
                if(fName == null)
                {
                    break;
                }
                var ext = Path.GetExtension(formFile.FileName).ToLower();
                var uploads = "media";
                var spon = "sponsor";

                var path = Path.Combine(hostingEnv.WebRootPath, uploads);
                Directory.CreateDirectory(path);

                path = Path.Combine(path, spon);
                Directory.CreateDirectory(path);

                path = Path.Combine(path, fName);
                test = Path.Combine(uploads, spon);
                //test = Path.Combine(path, fName);
                test = uploads + "/" + spon + "/";

                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);

                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            Media fp = new Media()
            {
                MediaPath = test,
                MediaName = fName,
                ItemId = 1, //temp to avoid errors  
                //MediaTypeId = 1
            };

            _context.Media.Add(fp);
            await _context.SaveChangesAsync();
            // return Ok(new { count = files.Count, size, filePath, fName, test });
            return RedirectToAction("UploadView");
        }
        
    }
}