using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CS3750TechnicalPrototypes.Data;
using CS3750TechnicalPrototypes.Models;
using CS3750TechnicalPrototypes.Models.ViewModels;
using Microsoft.AspNetCore.Session;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CS3750TechnicalPrototypes.Controllers
{
    public class AuctionController : Controller
    {
        private readonly AuctionContext _context;

        public AuctionController(AuctionContext context)
        {
            _context = context;
        }

        // GET: Auction
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.EndDateSortParm = sortOrder == "EndDate" ? "endDate_desc" : "EndDate";
            var auctions = from s in _context.Auctions
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                auctions = auctions.Where(s => s.AuctionName.Contains(searchString));
                                       
            }
            switch (sortOrder)
            {
                case "name_desc":
                    auctions = auctions.OrderByDescending(s => s.AuctionName);
                    break;
                case "Date":
                    auctions = auctions.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    auctions = auctions.OrderByDescending(s => s.StartDate);
                    break;
                case "EndDate":
                    auctions = auctions.OrderBy(s => s.EndDate);
                    break;
                case "endDate_desc":
                    auctions = auctions.OrderByDescending(s => s.EndDate);
                    break;

                default:
                    auctions = auctions.OrderBy(s => s.AuctionName);
                    break;
            }
            return View(auctions.ToList());
            
        }

        // GET: Auction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions.SingleOrDefaultAsync(m => m.AuctionId == id);
        

            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // GET: Auction/Create
        public IActionResult Create()
        {
            return View();
        }

 

        // POST: Auction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuctionID,AuctionName,StartDate,EndDate")] Auction auction) //re-add eventId
        {
            if (ModelState.IsValid)
            {
                _context.Add(auction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(auction);
        }




        // GET: Auction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions.SingleOrDefaultAsync(m => m.AuctionId == id);
            if (auction == null)
            {
                return NotFound();
            }
            return View(auction);
        }



        // POST: Auction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuctionId,AuctionName,StartDate,EndDateEventId")] Auction auction)
        {
            if (id != auction.AuctionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionExists(auction.AuctionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(auction);
        }

        // GET: Auction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions
                .SingleOrDefaultAsync(m => m.AuctionId == id);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // POST: Auction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auction = await _context.Auctions.SingleOrDefaultAsync(m => m.AuctionId == id);
            _context.Auctions.Remove(auction);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AuctionExists(int id)
        {
            return _context.Auctions.Any(e => e.AuctionId == id);
        }

    }
}
