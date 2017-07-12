using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS3750TechnicalPrototypes.Models;

namespace CS3750TechnicalPrototypes.Data
{
	public class DbInitializer
	{
		public static void Initialize(AuctionContext context)
		{
			context.Database.EnsureCreated();

			//Check for existing data. If found, exit method, else seed database.
			if (context.Auctions.Any())
				return;

			//Seed data
			//	No need to specify primary keys. Foreign keys need to be set up with LINQ.
			//	Tip: Use arrays with object-initializers. Fill out as much data as you can.
			//	Syntax: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-initialize-objects-by-using-an-object-initializer#example-1
			//	Tip: With "one-to-many" relations, start with the "one" then do the "many". (A game has many reviews. Games initalized then reviews initialized.)

			// --- Auction ---
			var auctions = new Auction[]
			{
				new Auction
				{
					AuctionName = "First Auction That Is Available",
					Description = "This is our first auction of the year! Currently open!",
					StartDate = DateTime.Parse("2017-7-8"),
					EndDate = DateTime.Parse("2017-8-1"),
					OpeningBid = 123.45,
					ItemID = 1
				},
				new Auction
				{
					AuctionName = "2 Nights Accommodations at Le Meridien Phuket Beach Resort + 2 Dinners",
					Description = "This is our second auction of last year. This auction is closed.",
					StartDate = DateTime.Parse("2016-1-2"),
					EndDate = DateTime.Parse("2016-2-3"),
					OpeningBid = 456.78,
					ItemID = 2

				},
				new Auction
				{
					AuctionName = "1 Night Accommodations at Andara Resort + In-suite BBQ w/ Champagne",
					Description = "This was the third auction of last year. This auction is closed.",
					StartDate = DateTime.Parse("2016-2-3"),
					EndDate = DateTime.Parse("2016-4-5"),
					OpeningBid = 901.23,
					ItemID = 3
				},
			};

			foreach (Auction a in auctions)
			{
				context.Auctions.Add(a);
			}
			context.SaveChanges();

			// --- Bidders ---
			var bidders = new Bidder[]
			{
				new Bidder
				{
					FirstName = "Michael",
					LastName = "Scott",
					PhoneNumber = "18004561234",
					EmailAddress = "mscott@dundermifflin.com",
					IsRegistered = false,
					Password = "none",
					Security = "none"
				},

				new Bidder
				{
					FirstName = "Barney",
					LastName = "Stinson",
					PhoneNumber = "450GetGood",
					EmailAddress = "Barney@BarneyStinson.com",
					IsRegistered = false,
					Password = "none",
					Security = "none"
				},

				new Bidder
				{
					FirstName = "Homer",
					LastName = "Simpson",
					PhoneNumber = "8881231337",
					EmailAddress = "homer@mrplow.com",
					IsRegistered = false,
					Password = "none",
					Security = "none"
				}
			};

			foreach (Bidder b in bidders)
			{
				context.Bidders.Add(b);
			}
			context.SaveChanges();

			// --- Items ---
			var items = new Item[]
			{
				new Item
				{
					SponsorId = 2,
					ItemName = "Five Night Stay at Barnacle Bay",
					ItemDescription = "Enjoy five unforgettable nights out in the lovely stench that is the exclusive Barnacle Bay! Winning this package will give your heart a warm feeling while your nose dies in disgust.",
					ItemValue = 100.00,
					OpeningBid = 80.00,
					BidIncrement = 10.00
					//Auction = context.Auctions.First(a => a.AuctionID == 1)
				},
				new Item
				{
					SponsorId = 2,
					ItemName = "Overnight Fishing Trip at Barnacle Bay",
					ItemDescription = "Enjoy an overnight trip, fishing barnacle bombs from Barnacle Bay! Winning this package will give your heart a warm feeling while your nose dies in disgust.",
					ItemValue = 80.00,
					OpeningBid = 70.00,
					BidIncrement = 10.00
					//Auction = context.Auctions.First(a => a.AuctionID == 2)
				},
				new Item
				{
					SponsorId = 1,
					ItemName = "Crazy Edd's Grand Fishing Day",
					ItemDescription = "Crazy Edd has offered to take you on the grandest fishing trip ever. It will exceed your imagination's attempts at imagining it. Don't pass up this once in a lifetime opportunity!",
					ItemValue = 200.00,
					OpeningBid = 180.00,
					BidIncrement = 10.00
					//Auction = context.Auctions.First(a => a.AuctionID == 3)
				},
			};

			foreach (Item i in items)
			{
				context.Items.Add(i);
			}
			context.SaveChanges();

			// --- BidHistory ---
			//Anytime between Jul 9 and Aug 01, 2017 to make it part of the seeded "active" auction..
			var bids = new BidHistory[]
			{
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-10"),
					BidAmount = 210.00,
					ItemId = 3,
					Auction = context.Auctions.First(a => a.AuctionID == 1),
					Bidder = context.Bidders.First(b => b.BidderID == 1)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-10"),
					BidAmount = 220.00,
					ItemId = 3,
					Auction = context.Auctions.First(a => a.AuctionID == 1),
					Bidder = context.Bidders.First(b => b.BidderID == 1)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-11"),
					BidAmount = 230.00,
					ItemId = 3,
					Auction = context.Auctions.First(a => a.AuctionID == 1),
					Bidder = context.Bidders.First(b => b.BidderID == 2)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-12"),
					BidAmount = 100.00,
					ItemId = 2,
					Auction = context.Auctions.First(a => a.AuctionID == 1),
					Bidder = context.Bidders.First(b => b.BidderID == 2)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-13"),
					BidAmount = 115.00,
					ItemId = 2,
					Auction = context.Auctions.First(a => a.AuctionID == 1),
					Bidder = context.Bidders.First(b => b.BidderID == 3)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-12"),
					BidAmount = 110.00,
					ItemId = 1,
					Auction = context.Auctions.First(a => a.AuctionID == 1),
					Bidder = context.Bidders.First(b => b.BidderID == 3)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-14"),
					BidAmount = 180.00,
					ItemId = 1,
					Auction = context.Auctions.First(a => a.AuctionID == 1),
					Bidder = context.Bidders.First(b => b.BidderID == 1)
				},
			};

			foreach (BidHistory b in bids)
			{
				context.BidHistory.Add(b);
			}
			context.SaveChanges();

		}
	}
}
