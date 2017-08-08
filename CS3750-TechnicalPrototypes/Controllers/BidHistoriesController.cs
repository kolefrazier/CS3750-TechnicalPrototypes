using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS3750TechnicalPrototypes.Data;
using CS3750TechnicalPrototypes.Models;
using CS3750TechnicalPrototypes.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CS3750TechnicalPrototypes.Controllers
{
    public class BidHistoriesController : Controller
    {
        private readonly AuctionContext _context;

        public BidHistoriesController(AuctionContext context)
        {
            _context = context;
        }

        // GET: BidHistories
        public async Task<IActionResult> Index(int? id, string sortOrder, string searchString)
        {
            if (id == null)
            {
                return RedirectToAction("ViewActiveAuctions");
                // return NotFound();
            }
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.BidSortParm = sortOrder == "hBid" ? "hBid_desc" : "hBid";


            List<ItemMedia> im = new List<ItemMedia>();


            // var history = await _context.BidHistory.ToListAsync();
            var auction = await _context.Auctions.Where(x => x.AuctionId == id).SingleOrDefaultAsync();
            var items = await _context.Items.Where(x => x.AuctionId == id).ToListAsync();
            var carousel = await _context.Media.Where(x => x.ItemId == 0).ToListAsync();
           

            foreach(var item in items)
            {
                var media = await _context.Media.Where(x => x.ItemId == item.ItemId).ToListAsync();
                var highestBid = await _context.BidHistory.Where(x => x.ItemId == item.ItemId).MaxAsync(y=> y.BidAmount);
                
                ItemMedia modelItem = new ItemMedia()
                {
                    Item = item,
                    Media = media,
                    highestBid = highestBid
                };

                im.Add(modelItem);
            }

            AuctionItem model = new AuctionItem
            {
                Auction = auction,
                Carousel = carousel,
                Items = im
            };

            if (!String.IsNullOrEmpty(searchString))
            {
                model.Items = model.Items.Where(s => s.Item.ItemName.Contains(searchString)
                                       || s.highestBid.ToString() == searchString);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    model.Items = model.Items.OrderByDescending(s => s.Item.ItemName);
                    break;
                case "hBid":
                    model.Items = model.Items.OrderBy(s => s.highestBid);
                    break;
                case "hBid_desc":
                    model.Items = model.Items.OrderByDescending(s => s.highestBid);
                    break;
                default:
                    model.Items = model.Items.OrderBy(s => s.Item.ItemName);
                    break;
            }
           
           

            return View(model);
        }

        //TODO: Fix Bids by Auction page
        /*// GET: Bids by Auction, BidHistories/BidsByAuction/5
		[HttpGet]
		public IActionResult BidsByAuction(int? id)
		{
			if(id != null && (int)id > 0)
			{
				var BidsByAuction = _context.BidHistory.Where(b => b.AuctionId == (int)id).ToList<BidHistory>();

				List<BidDetails> BidDetailsCollection = new List<BidDetails>();

				foreach(BidHistory b in BidsByAuction)
				{
					BidDetails tmp = new BidDetails
					{
						BidHistory = b,
						Item = _context.Items.First(i => i.ItemId == b.ItemId),
						Bidder = b.Bidder
					};
					BidDetailsCollection.Add(tmp);
				}
				return View(BidDetailsCollection);
			} else
			{
				return RedirectToAction("Index");
			}
		}*/

        //takes bidder id as input
        public IActionResult ViewBidHistory(int? id)
        {
			if(id == null)
			{
				id = HttpContext.Session.GetInt32("UserId");
			}

          //  List<BidHistory> model = new List<BidHistory>();
            var history =  _context.BidHistory.Where(x => x.BidderId == id).ToList();
            foreach(BidHistory bh in history)
            {
                var items = _context.Items.Where(x => x.ItemId == bh.ItemId).ToList();
                bh.Item = items;
            }

            return View(history);
        }


        public IActionResult ViewActiveAuctions()
        {
			var now = DateTime.Now;
            var auctions = _context.Auctions.Where(a => a.StartDate <= now && a.EndDate > now).ToList();
            return View(auctions);
        }

        // GET: BidHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidHistory = await _context.BidHistory
                .SingleOrDefaultAsync(m => m.BidHistoryId == id);
            if (bidHistory == null)
            {
                return NotFound();
            }

            return View(bidHistory);
        }

        // GET: BidHistories/Create
        public IActionResult Create(int? ItemId, int? AuctionId)
        {
            if (ItemId != null && ItemId > 0 && AuctionId != null && AuctionId > 0)
            {
                var media = _context.Media.Where(x => x.ItemId == ItemId).ToList();
                var item = _context.Items.Where(x => x.ItemId == ItemId).SingleOrDefault();
                BidCreateViewModel tmp = new BidCreateViewModel
                {
                    ItemId = (int)ItemId,
                    AuctionId = (int)AuctionId,
                    Media = media,
                    Item = item,
                    ItemCurrentBid = GetMaxBidByItemId(item.ItemId),
                    ItemMinimumBid = GetMinimumBidByItemId(item.ItemId)
                };
                return View(tmp);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: BidHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("BidHistoryId,BidDate,BidAmount,ItemId")] BidHistory bidHistory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(bidHistory);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(bidHistory);
        //}

        // POST: BidHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidDate,BidAmount,ItemId,AuctionId,ItemCurrentBid,ItemMinimumBid,Bidder_FirstName,Bidder_LastName,Bidder_PhoneNumber,Bidder_EmailAddress")] BidCreateViewModel bid)
        {
            //Retrieve this stuff just incase of validation errors:
            var media = _context.Media.Where(x => x.ItemId == bid.ItemId).ToList();
            var item = _context.Items.Where(x => x.ItemId == bid.ItemId).SingleOrDefault();

            bool HasErrors = false;
            double ItemMinimumBid = GetMinimumBidByItemId(bid.ItemId);
            if(bid.BidAmount < ItemMinimumBid)
            {
                ModelState.AddModelError("BidAmount", "Your bid must be greater than or equal to the minimum bid.");
                //return RedirectToAction("Create", new { ItemId = bid.ItemId, AuctionId = bid.AuctionId });

                //Re-retrieve some values to prevent null exceptions
                bid.Media = media;
                bid.Item = item;
                HasErrors = true;
            }

            //Manual validations... Please don't look too close at the laziness.
            if(String.IsNullOrWhiteSpace(bid.Bidder_FirstName))
            {
                ModelState.AddModelError("Bidder_FirstName", "Please enter your first name.");
                HasErrors = true;
            }
            if (string.IsNullOrWhiteSpace(bid.Bidder_LastName))
            {
                ModelState.AddModelError("Bidder_LastName", "Please enter your last name.");
                HasErrors = true;
            }
            if (String.IsNullOrWhiteSpace(bid.Bidder_PhoneNumber))
            {
                ModelState.AddModelError("Bidder_PhoneNumber", "Please a valid phone number.");
                HasErrors = true;
            }
            if (String.IsNullOrWhiteSpace(bid.Bidder_EmailAddress))
            {
                ModelState.AddModelError("Bidder_EmailAddress", "Please a valid email address.");
                HasErrors = true;
            }

            if (HasErrors)
            {
                return View(bid);
            }

            Bidder newBidder = new Bidder
            {
                FirstName = bid.Bidder_FirstName,
                LastName = bid.Bidder_LastName,
                PhoneNumber = bid.Bidder_PhoneNumber,
                EmailAddress = bid.Bidder_EmailAddress,
                IsRegistered = false,
                Password = "derp",
                Security = "derpaderp"
            };

            _context.Bidders.Add(newBidder);
            await _context.SaveChangesAsync();

            BidHistory newBid = new BidHistory
            {
                //Auction = _context.Auctions.SingleOrDefault(a => a.AuctionId == 1), //Temp debug workaround because AuctionId keeps passing in as 0.
                //Auction = _context.Auctions.SingleOrDefault(a => a.AuctionID == bid.AuctionId),
                BidDate = bid.BidDate,
                BidAmount = bid.BidAmount,
                ItemId = bid.ItemId,
                BidderId = newBidder.BidderID
                //Bidder = _context.Bidders.FirstOrDefault(b => b.FirstName == bid.Bidder_FirstName && b.LastName == bid.Bidder_LastName)
            };

            var AddedBid = _context.BidHistory.Add(newBid);
            int SavedValue = await _context.SaveChangesAsync();

            return RedirectToAction("Index", "BidHistories", new { id = bid.AuctionId });

        }

        // GET: BidHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidHistory = await _context.BidHistory.SingleOrDefaultAsync(m => m.BidHistoryId == id);
            if (bidHistory == null)
            {
                return NotFound();
            }
            return View(bidHistory);
        }

        // POST: BidHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidHistoryId,BidDate,BidAmount,ItemId")] BidHistory bidHistory)
        {
            if (id != bidHistory.BidHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bidHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidHistoryExists(bidHistory.BidHistoryId))
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
            return View(bidHistory);
        }

        // GET: BidHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidHistory = await _context.BidHistory
                .SingleOrDefaultAsync(m => m.BidHistoryId == id);
            if (bidHistory == null)
            {
                return NotFound();
            }

            return View(bidHistory);
        }

        // POST: BidHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bidHistory = await _context.BidHistory.SingleOrDefaultAsync(m => m.BidHistoryId == id);
            _context.BidHistory.Remove(bidHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BidHistoryExists(int id)
        {
            return _context.BidHistory.Any(e => e.BidHistoryId == id);
        }

        //Utility Methods
        private Item GetItemById(int id)
        {
            return _context.Items.Where(i => i.ItemId == id).First();
        }

        //Max Bid is basically the current bid (the highest, valid bid)
        private double GetMaxBidByItemId(int id)
        {
            var BidHistoryById = _context.BidHistory
                .Where(b => b.ItemId == id)
                .Max(x => x.BidAmount);

            return BidHistoryById;
        }

        //Current Bid + Bid Increment
        private double GetMinimumBidByItemId(int id)
        {
            Item SelectedItem = GetItemById(id);
            double CurrentHighestBid = GetMaxBidByItemId(id);
            if(CurrentHighestBid <= 0)
            {
                return SelectedItem.OpeningBid + SelectedItem.BidIncrement;
            } else
            {
                return CurrentHighestBid + SelectedItem.BidIncrement;
            }
        }
    }
}
