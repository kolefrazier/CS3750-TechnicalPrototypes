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
					StartDate = DateTime.Parse("2017-7-8"),
					EndDate = DateTime.Parse("2017-8-1"),
					OpeningBid = 123.45
				},
				new Auction
				{
					AuctionName = "Second Auction In The Past",
					StartDate = DateTime.Parse("2016-1-2"),
					EndDate = DateTime.Parse("2016-2-3"),
					OpeningBid = 456.78
				},
				new Auction
				{
					AuctionName = "Third Auction In The Past Again",
					StartDate = DateTime.Parse("2016-2-3"),
					EndDate = DateTime.Parse("2016-4-5"),
					OpeningBid = 901.23
				},
			};

			foreach (Auction a in auctions)
			{
				context.Auctions.Add(a);
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
					Auction = context.Auctions.First(a => a.AuctionID == 1)
				},
				new Item
				{
					SponsorId = 2,
					ItemName = "Overnight Fishing Trip at Barnacle Bay",
					ItemDescription = "Enjoy an overnight trip, fishing barnacle bombs from Barnacle Bay! Winning this package will give your heart a warm feeling while your nose dies in disgust.",
					ItemValue = 80.00,
					Auction = context.Auctions.First(a => a.AuctionID == 1)
				},
				new Item
				{
					SponsorId = 1,
					ItemName = "Crazy Edd's Grand Fishing Day",
					ItemDescription = "Crazy Edd has offered to take you on the grandest fishing trip ever. It will exceed your imagination's attempts at imagining it. Don't pass up this once in a lifetime opportunity!",
					ItemValue = 200.00,
					Auction = context.Auctions.First(a => a.AuctionID == 1)
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
					Auction = context.Auctions.First(a => a.AuctionID == 1)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-10"),
					BidAmount = 220.00,
					ItemId = 3,
					Auction = context.Auctions.First(a => a.AuctionID == 1)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-11"),
					BidAmount = 230.00,
					ItemId = 3,
					Auction = context.Auctions.First(a => a.AuctionID == 1)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-12"),
					BidAmount = 100.00,
					ItemId = 2,
					Auction = context.Auctions.First(a => a.AuctionID == 1)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-13"),
					BidAmount = 115.00,
					ItemId = 2,
					Auction = context.Auctions.First(a => a.AuctionID == 1)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-12"),
					BidAmount = 110.00,
					ItemId = 1,
					Auction = context.Auctions.First(a => a.AuctionID == 1)
				},
				new BidHistory
				{
					BidDate = DateTime.Parse("2017-7-14"),
					BidAmount = 180.00,
					ItemId = 1,
					Auction = context.Auctions.First(a => a.AuctionID == 1)
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
