using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS3750TechnicalPrototypes.Data;
using CS3750TechnicalPrototypes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;


namespace CS3750TechnicalPrototypes.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AuctionContext _context;

        public ItemsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.Items.Include(i => i.Auction);
            return View(await auctionContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Auction)
                .SingleOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,sponsorID,ItemName,ItemDescription,ItemValue,OpeningBid,BidIncrement,AuctionId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // ViewData["AuctionId"] = new SelectList(_context.Auctions, "AuctionID", "AuctionID", item.AuctionId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.SingleOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            PopulateDropDownList();
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,sponsorID,ItemName,ItemDescription,ItemValue,OpeningBid,BidIncrement,AuctionId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            //  ViewData["AuctionId"] = new SelectList(_context.Auctions, "AuctionID", "AuctionID", item.AuctionId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Auction)
                .SingleOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.SingleOrDefaultAsync(m => m.ItemId == id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }


        //Populates the ViewBag with all currently active auctions
        private void PopulateDropDownList()
        {
            var AuctionsQuery = from i in _context.Auctions
                                select i;
            ViewBag.AuctionId = new SelectList(AuctionsQuery.AsNoTracking(), "AuctionId", "AuctionName");
        }

        private void PopulateSponsors()
        {
            var SponsorsQuery = from i in _context.Sponsors
                                select i;
            ViewBag.sponsorID = new SelectList(SponsorsQuery.AsNoTracking(), "sponsorID", "sponsorName");
        }

    }
}
