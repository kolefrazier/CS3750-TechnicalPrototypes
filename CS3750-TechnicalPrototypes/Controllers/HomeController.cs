using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CS3750TechnicalPrototypes.Models;
using CS3750TechnicalPrototypes.Data;

namespace CS3750TechnicalPrototypes.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuctionContext _context;

        public HomeController(AuctionContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Media> model = new List<Media>();
            model = _context.Media.ToList();
            //model.ElementAt(0).Path
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult AdminControl()
        {
           // ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
  
    }
}
