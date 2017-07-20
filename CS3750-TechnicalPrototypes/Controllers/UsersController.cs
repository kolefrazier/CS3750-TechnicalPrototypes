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
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

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
		public IActionResult Index()
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
			if(!string.IsNullOrEmpty(bidder.EmailAddress) && !string.IsNullOrEmpty(bidder.Password))
			{
				//Check if user already exists in table as a non-registered bidder. (Someone who bid without registering.)
				var UserExists = _context.Bidders.Any(b => b.EmailAddress == bidder.EmailAddress);

				Bidder MatchedUser;
				if (UserExists)
					MatchedUser = _context.Bidders.First(b => b.EmailAddress == bidder.EmailAddress);
				else
					MatchedUser = null;

				//If the person exists and is registered, return an "already exists" error.
				if (MatchedUser != null && MatchedUser.IsRegistered == true)
				{
					ModelState.AddModelError("EmailAddress", "This email address is already in use.");
					return View();
				} else if (MatchedUser != null && MatchedUser.IsRegistered == false)
				{
					//Else if the person exists and is NOT registered, set them up as such.
					//	Don't ever do this in real life, please.
					//	This is a terrible thing to do, but works for this prototype's simplicity.
					MatchedUser.IsRegistered = true; //Set to true.
					MatchedUser.Password = bidder.Password;
					MatchedUser.Security = bidder.Security;
					//If we had an email provider, you could force someone to verify their email address before setting the above values.
				} else
				{
					//Next, handle a non-existant user.
					//The registration/creation page isn't taking everything in, so we need to setup a new object first.
					Bidder NewBidder = new Bidder()
					{
						FirstName = bidder.FirstName,
						LastName = bidder.LastName,
						PhoneNumber = bidder.PhoneNumber,
						EmailAddress = bidder.EmailAddress,
						IsRegistered = true,
						Password = bidder.Password,
						Security = bidder.Security
					};
					_context.Add(NewBidder);
					await _context.SaveChangesAsync();
					return RedirectToAction("Login");
				}
			}

			//Return view if errors occurred, etc. Default state.
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
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		// POST: Users/Login
		[HttpPost]
		public IActionResult Login([Bind("EmailAddress, Password")] UserLoginViewModel user)
		{
			//Find user, force the Role to load if one is found.
			var MatchedUser = _context.Bidders
				.Where(u => u.EmailAddress == user.EmailAddress)
				.Include(x => x.Role)
				.First();

			if(MatchedUser == null || MatchedUser.IsRegistered == false)
			{
				ModelState.AddModelError("Model", "Invalid login.");
				return View();
			} else if (MatchedUser.Password == user.Password)
			{
				SetLoginInformation(MatchedUser);
				return RedirectToAction("Index", "Auction");
			} else
			{
				ModelState.AddModelError("Model", "An unknown error occurred. Please try again.");
				return View();
			}
		}

		// GET: Users/Logout
		public IActionResult Logout()
		{
			ClearLoginInformation();
			return View();
		}

		// GET: Users/EmailConfirmation
		public IActionResult EmailConfirmation()
		{
			return View();
		}

		public void SetLoginInformation(Bidder user)
		{
			//Totally not susceptible to cookie modification.........
			HttpContext.Session.SetString("LoggedIn", "true");
			HttpContext.Session.SetInt32("UserId", user.BidderID);
			HttpContext.Session.SetInt32("RoleId", RoleEnumToInt(user.Role.UserRole));
			HttpContext.Session.SetString("RoleName", RoleEnumToString(user.Role.UserRole));
			HttpContext.Session.SetString("UserEmail", user.EmailAddress);
		}

		public void RetrieveLoginInformation()
		{
			HttpContext.Session.LoadAsync();
		}

		public void ClearLoginInformation()
		{
			HttpContext.Session.Clear();
		}

		//Mmm-mmm. Ain't that some fine, non-dynamic ugliness.
		private string RoleEnumToString(Roles role)
		{
			switch (role)
			{
				case (Roles.Administrator):
					return "Administrator";
				case (Roles.OfficeWorker):
					return "OfficeWorker";
				case (Roles.User):
					return "User";
				default:
					return "User";
			}
		}

		private int RoleEnumToInt(Roles role)
		{
			switch (role)
			{
				case (Roles.Administrator):
					return 1;
				case (Roles.OfficeWorker):
					return 2;
				case (Roles.User):
					return 3;
				default:
					return 3;
			}
		}

		private bool BidderExists(int id)
		{
			return _context.Bidders.Any(e => e.BidderID == id);
		}
	}
}
