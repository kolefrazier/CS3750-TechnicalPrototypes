using CS3750TechnicalPrototypes.Data;
using CS3750TechnicalPrototypes.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Post(List<IFormFile> files, int itemId)
        {
            var fName = "";
            var fPath = "";
            var ext = "";
            var shortPath = "";

            if (files.Count == 0)
            {
                return RedirectToAction("UploadView");
            }

            //TODO: Create method for handling file paths
            foreach (var formFile in files)
            {
                fName = Path.GetFileName(formFile.FileName);
                ext = Path.GetExtension(formFile.FileName).ToLower();

                //create our folder directory in the wwwroot folder
                fPath = createFilePath(itemId, fName, ref shortPath);

                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(fPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // create media model to store image data in db
            Media image = new Media()
            {
                MediaPath = shortPath,
                MediaName = fName,
                ItemId = itemId,
            };


            _context.Media.Add(image);
            await _context.SaveChangesAsync();
            // return Ok(new { count = files.Count, size, filePath, fName, test });

            if (itemId == 0)
            {
                return RedirectToAction("UploadView");
            }else
            {
                return RedirectToAction("UploadItemImage", "Items", new { id = itemId });
            }
        }

        public string createFilePath(int id, string fName, ref string shortPath)
        {
            string media = "media";
            string carousel = "carousel";
            string item = "item";

            //create top level media directory
            var path = Path.Combine(hostingEnv.WebRootPath, media);
            Directory.CreateDirectory(path);
            shortPath = "\\" + media;

            //if our id is 0 that means this is a carousel image
            if (id == 0)
            {
                path = Path.Combine(path, carousel);
                Directory.CreateDirectory(path);
                shortPath += "\\" + carousel;
            }
            else
            {
                path = Path.Combine(path, item);
                Directory.CreateDirectory(path);
                shortPath += "\\" + item;

            }

            path = Path.Combine(path, fName);
            shortPath += "\\" + fName;

            return path;
        }

    }
}