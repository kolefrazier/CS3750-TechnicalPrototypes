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
		public async Task<IActionResult> Index()
		{
			List<BidHistory> model = new List<BidHistory>();


			var auctions = await _context.Auctions.ToListAsync();
		   // var items = await _context.Items.ToListAsync();

			foreach (Auction auc in auctions)
			{
				//Only way I could come up with to get the items
				//Because items is enumerable in the auction model I have to make a new list
				//and place a single item within in order to satisfy requirements
				List<Item> itList = new List<Item>();
				Item temp = await _context.Items.SingleOrDefaultAsync(x => x.ItemId == auc.ItemID);
				itList.Add(temp);
				auc.Item = itList;

				BidHistory history = new BidHistory()
				{
					Auction = auc
				};
				model.Add(history);
			}

			return View(model);
		}

		// GET: Bids by Auction, BidHistories/BidsByAuction/5
		[HttpGet]
		public IActionResult BidsByAuction(int? id)
		{
			if(id != null && (int)id > 0)
			{
				var BidsByAuction = _context.BidHistory.Where(b => b.Auction.AuctionID == (int)id).ToList<BidHistory>();

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
			if(ItemId != null && ItemId > 0 && AuctionId != null && AuctionId > 0)
			{
				BidCreateViewModel tmp = new BidCreateViewModel
				{
					ItemId = (int)ItemId,
					AuctionId = (int)AuctionId
				};
				return View(tmp);
			} else
			{
				return View();
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
		public async Task<IActionResult> Create([Bind("BidDate,BidAmount,ItemId,AuctionId,Bidder_FirstName,Bidder_LastName,Bidder_PhoneNumber,Bidder_EmailAddress")] BidCreateViewModel bid)
		{
			Item item = _context.Items.SingleOrDefault(i => i.ItemId == 1); // bid.ItemId); //Temp debug workaround because ItemId keeps passing in as 0.

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
				Auction = _context.Auctions.SingleOrDefault(a => a.AuctionID == 1), //Temp debug workaround because AuctionId keeps passing in as 0.
				//Auction = _context.Auctions.SingleOrDefault(a => a.AuctionID == bid.AuctionId),
				BidDate = bid.BidDate,
				BidAmount = bid.BidAmount,
				ItemId = bid.ItemId,
				Bidder = _context.Bidders.FirstOrDefault(b => b.FirstName == bid.Bidder_FirstName && b.LastName == bid.Bidder_LastName)
			};

			_context.BidHistory.Add(newBid);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "BidHistories");
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
	}
}
