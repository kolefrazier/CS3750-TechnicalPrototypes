using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS3750TechnicalPrototypes.Data;
using CS3750TechnicalPrototypes.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using CS3750TechnicalPrototypes.Models.ViewModels;

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
            //var auctionContext = _context.Items.Include(i => i.Auction);
            //return View(await auctionContext.ToListAsync());
            List<ItemMedia> im = new List<ItemMedia>();

            var items = await _context.Items.ToListAsync();

            foreach (var item in items)
            {
                var media = await _context.Media.Where(x => x.ItemId == item.ItemId).ToListAsync();

                ItemMedia modelItem = new ItemMedia
                {
                    Item = item,
                    Media = media
                };
                im.Add(modelItem);
            }

            return View(im);
        }

        public IActionResult UploadItemImage(int id)
        {
            var item = _context.Items.Where(x => x.ItemId == id).FirstOrDefault();

            return View(item);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //get the item
            var item = await _context.Items.Include(i => i.Auction).SingleOrDefaultAsync(m => m.ItemId == id);

            //get the items media
            var media = await _context.Media.Where(x => x.ItemId == item.ItemId).ToListAsync();

            ItemMedia model = new ItemMedia
            {
                Item = item,
                Media = media
            };
           // im.Add(modelItem);


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
            PopulateSponsors();
			PopulateCategoriesDropDown();

			return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,sponsorID,CategoryId,ItemName,ItemDescription,ItemValue,OpeningBid,BidIncrement,AuctionId")] Item item)
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
            PopulateSponsors();
			PopulateCategoriesDropDown();


			return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,sponsorID,CategoryId,ItemName,ItemDescription,ItemValue,OpeningBid,BidIncrement,AuctionId")] Item item)
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

		private void PopulateCategoriesDropDown()
		{
			var CategoriesQuery = from i in _context.Categories
								  select i;
			ViewBag.CategoryId = new SelectList(CategoriesQuery.AsNoTracking(), "CategoryId", "Name");
		}

		// --- Item Bid History Methods ---
		private Item GetItemById(int id)
		{
			return _context.Items.Where(i => i.ItemId == id).First();
		}

		private double GetMaxBidByItemId(int id)
		{
			var BidHistoryById = _context.BidHistory
				.Where(b => b.ItemId == id)
				.Max(x => x.BidAmount);

			return BidHistoryById;
		}

        private double GetMinimumBidByItemId(int id)
        {
            Item SelectedItem = GetItemById(id);
            double CurrentHighestBid = GetMaxBidByItemId(id);
            if (CurrentHighestBid <= 0)
            {
                return SelectedItem.OpeningBid + SelectedItem.BidIncrement;
            }
            else
            {
                return CurrentHighestBid + SelectedItem.BidIncrement;
            }
        }
    }
}
