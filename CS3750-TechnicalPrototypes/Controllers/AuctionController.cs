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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Auctions.ToListAsync());
        }

        // GET: Auction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions.SingleOrDefaultAsync(m => m.AuctionID == id);
            // var item = await _context.Items.SingleOrDefaultAsync(x => x.ItemId == auction.ItemID);

            

            if (auction == null)
            {
                return NotFound();
            }

            //AuctionDetails aucDet = new AuctionDetails()
            //{
            //    Auction = auction,
            //    Item = item
            //};

            return View(auction);
        }

        // GET: Auction/Create
        public IActionResult Create()
        {
           // PopulateDropDownList();
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

            var auction = await _context.Auctions.SingleOrDefaultAsync(m => m.AuctionID == id);
            if (auction == null)
            {
                return NotFound();
            }
           // PopulateDropDownList();
            return View(auction);
        }

        //private void PopulateDropDownList()
        //{
        //    var ItemsQuery = from i in _context.Items
        //                     select i;
        //    ViewBag.ItemId = new SelectList(ItemsQuery.AsNoTracking(), "ItemId", "ItemName");
        //}

        // POST: Auction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuctionID,AuctionName,StartDate,EndDateEventId")] Auction auction)
        {
            if (id != auction.AuctionID)
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
                    if (!AuctionExists(auction.AuctionID))
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
       //     PopulateDropDownList();
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
                .SingleOrDefaultAsync(m => m.AuctionID == id);
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
            var auction = await _context.Auctions.SingleOrDefaultAsync(m => m.AuctionID == id);
            _context.Auctions.Remove(auction);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AuctionExists(int id)
        {
            return _context.Auctions.Any(e => e.AuctionID == id);
        }

    }
}
