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

			var auctions = new Auction[]
			{
				new Auction { AuctionName = "One auction, ha ha ha" },
				new Auction { AuctionName = "Two auction, ha ha ha" }
			};

			foreach (Auction a in auctions)
			{
				context.Auctions.Add(a);
			}
		}
	}
}
