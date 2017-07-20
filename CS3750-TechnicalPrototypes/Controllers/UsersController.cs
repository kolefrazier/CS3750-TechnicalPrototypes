using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS3750TechnicalPrototypes.Data;
using CS3750TechnicalPrototypes.Models;
using Microsoft.AspNetCore.Session;

namespace CS3750TechnicalPrototypes.Controllers
{
    public class UsersController : Controller
    {
        private readonly AuctionContext _context;

        public UsersController(AuctionContext context)
        {
            _context = context;    
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
			//return View(await _context.Bidders.ToListAsync());
			return RedirectToAction("Login");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidder = await _context.Bidders
                .SingleOrDefaultAsync(m => m.BidderID == id);
            if (bidder == null)
            {
                return NotFound();
            }

            return View(bidder);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidderID,FirstName,LastName,PhoneNumber,EmailAddress,IsRegistered,Password,Security")] Bidder bidder)
        {
            //if (ModelState.IsValid)
			if(!string.IsNullOrEmpty(bidder.EmailAddress) && !string.IsNullOrEmpty(bidder.Password))
            {
				if(_context.Bidders.First(b => b.EmailAddress == bidder.EmailAddress).IsRegistered == true)
				{
					ModelState.AddModelError("EmailAddress", "This email address is already in use.");
					return View();
				} else
				{
					_context.Add(bidder);
					await _context.SaveChangesAsync();
					return RedirectToAction("Login");
				}
            }
            return View(bidder);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidder = await _context.Bidders.SingleOrDefaultAsync(m => m.BidderID == id);
            if (bidder == null)
            {
                return NotFound();
            }
            return View(bidder);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidderID,FirstName,LastName,PhoneNumber,EmailAddress,IsRegistered,Password,Security")] Bidder bidder)
        {
            if (id != bidder.BidderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bidder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidderExists(bidder.BidderID))
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
            return View(bidder);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidder = await _context.Bidders
                .SingleOrDefaultAsync(m => m.BidderID == id);
            if (bidder == null)
            {
                return NotFound();
            }

            return View(bidder);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bidder = await _context.Bidders.SingleOrDefaultAsync(m => m.BidderID == id);
            _context.Bidders.Remove(bidder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

		// GET: Users/Login
		public IActionResult Login()
		{
			return View();
		}

		// GET: Users/Logout
		public IActionResult Logout()
		{
			return View();
		}

        private bool BidderExists(int id)
        {
            return _context.Bidders.Any(e => e.BidderID == id);
        }
    }
}
