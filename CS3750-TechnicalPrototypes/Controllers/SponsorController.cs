using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CS3750TechnicalPrototypes.Controllers
{
    public class SponsorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}