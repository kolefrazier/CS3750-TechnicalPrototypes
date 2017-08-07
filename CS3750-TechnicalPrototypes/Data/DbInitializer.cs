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
                    AuctionName = "25th Year Anniversary",
                    Description = "This is our first auction of the year! Currently open!",
                    StartDate = DateTime.Today.AddDays(-3),
                    EndDate = DateTime.Today.AddDays(4),
					//OpeningBid = 58500,
					//ItemID = 1
				},
                new Auction
                {
                    AuctionName = "PHBGTU Support Fund",
                    Description = "This is our second auction of last year. This auction is closed.",
                    StartDate = DateTime.Parse("2017-1-2"),
                    EndDate = DateTime.Parse("2017-2-3"),
					//OpeningBid = 36000,
					//ItemID = 2

				},
                new Auction
                {
                    AuctionName = "The 'I needed a third auction' auction",
                    Description = "This was the third auction of last year. This auction is closed.",
                    StartDate = DateTime.Parse("2016-2-3"),
                    EndDate = DateTime.Parse("2016-4-5"),
					//OpeningBid = 901.23,
					//ItemID = 3
				},
            };

            foreach (Auction a in auctions)
            {
                context.Auctions.Add(a);
            }
            context.SaveChanges();

            // --- Roles ---
            var roles = new Role[]
            {
                new Role
                {
                    ShortDescription = "Administrator",
                    UserRole = Roles.Administrator
                },
                new Role
                {
                    ShortDescription = "Office Staff",
                    UserRole = Roles.OfficeWorker
                },
                new Role
                {
                    ShortDescription = "Normal User",
                    UserRole = Roles.User
                }
            };

            foreach (Role r in roles)
            {
                context.Roles.Add(r);
            }
            context.SaveChanges();

			// --- Categories ---
			var categories = new Category[]
			{
				new Category
				{
					Name = "Trip",
					Description = "A destination vacation!"
				},
				new Category
				{
					Name = "Hotel",
					Description = "Hotel Room Packages and Specials"
				},
				new Category
				{
					Name = "Food",
					Description = "A prize that you can eat."
				}
			};

			foreach (Category c in categories)
			{
				context.Categories.Add(c);
			}
			context.SaveChanges();

            // --- Bidders ---
            var bidders = new Bidder[]
            {
				//Users with Role level 1 (admin)
				new Bidder
                {
                    FirstName = "The",
                    LastName = "Administrator",
                    PhoneNumber = "18000023545",
                    EmailAddress = "admin@phbgtu.com",
                    IsRegistered = true,
                    Password = "Password123",
                    Security = "none",
                    Role = context.Roles.First(r => r.RoleID == 1)
                },

                new Bidder
                {
                    FirstName = "AdminWithA",
                    LastName = "ShortLogin",
                    PhoneNumber = "18000023545",
                    EmailAddress = "a@a.com",
                    IsRegistered = true,
                    Password = "a",
                    Security = "none",
                    Role = context.Roles.First(r => r.RoleID == 1)
                },

				//Users with Role level 2 (office worker)
				new Bidder
                {
                    FirstName = "Sam",
                    LastName = "OfficeWorker",
                    PhoneNumber = "18000023545",
                    EmailAddress = "sam@gmail.com",
                    IsRegistered = true,
                    Password = "a",
                    Security = "none",
                    Role = context.Roles.First(r => r.RoleID == 2)
                },

				//Users with Role level 3 (normal users)
				new Bidder
                {
                    FirstName = "Thomas",
                    LastName = "The Tank",
                    PhoneNumber = "18006549871",
                    EmailAddress = "thomas@trains.com",
                    IsRegistered = true, //IS REGISTERED
					Password = "abcd123",
                    Security = "none",
                    Role = context.Roles.First(r => r.RoleID == 3)
                },

                new Bidder
                {
                    FirstName = "Michael",
                    LastName = "Scott",
                    PhoneNumber = "18004561234",
                    EmailAddress = "mscott@dundermifflin.com",
                    IsRegistered = false,
                    Password = "none",
                    Security = "none",
                    Role = context.Roles.First(r => r.RoleID == 3)
                },

                new Bidder
                {
                    FirstName = "Barney",
                    LastName = "Stinson",
                    PhoneNumber = "450GetGood",
                    EmailAddress = "Barney@BarneyStinson.com",
                    IsRegistered = false,
                    Password = "none",
                    Security = "none",
                    Role = context.Roles.First(r => r.RoleID == 3)
                },

                new Bidder
                {
                    FirstName = "Homer",
                    LastName = "Simpson",
                    PhoneNumber = "8881231337",
                    EmailAddress = "homer@mrplow.com",
                    IsRegistered = false,
                    Password = "none",
                    Security = "none",
                    Role = context.Roles.First(r => r.RoleID == 3)
                },

                new Bidder
                {
                    FirstName = "Tom",
                    LastName = "Hanks",
                    PhoneNumber = "1800TomHank",
                    EmailAddress = "tom@tom.com",
                    IsRegistered = true,
                    Password = "abcdEfg123",
                    Security = "blah",
                    Role = context.Roles.First(r => r.RoleID == 3)
                }
            };

            foreach (Bidder b in bidders)
            {
                context.Bidders.Add(b);
            }
            context.SaveChanges();

            // ---Sponsors---
            var sponsors = new Sponsor[]
            {
                new Sponsor
                {
                    //sponsorID = 2,
                    sponsorName = "Barnacle Bay",
                    sponsorEmail = "barnaclebayresort@gmail.com"
                },

                new Sponsor
                {
                    //sponsorID = 1,
                    sponsorName = "Crazy Edd",
                    sponsorEmail = "crazyedfishing@yahoo.com"
                },

                new Sponsor
                {
                    //sponsorID = 3,
                    sponsorName = "Mystery Shippers",
                    sponsorEmail = "unknownship@gmail.com"
                },
            };

            foreach (Sponsor s in sponsors)
            {
                context.Sponsors.Add(s);
            }
            context.SaveChanges();

            // --- Items ---
            var items = new Item[]
            {
                new Item
                {
                    ItemName = "Five Night Stay at Barnacle Bay",
                    ItemDescription = "Enjoy five unforgettable nights out in the lovely stench that is the exclusive Barnacle Bay! Winning this package will give your heart a warm feeling while your nose dies in disgust.",
                    ItemValue = 100.00,
                    OpeningBid = 80.00,
                    BidIncrement = 10.00,
                    Auction = context.Auctions.First(a => a.AuctionId == 1),
                    Sponsor = context.Sponsors.First(s => s.sponsorID == 2),
					Category = context.Categories.First(c => c.CategoryId == 1),
					//Media = null,
					BidHistory = null
                },
                new Item
                {
                    ItemName = "Overnight Fishing Trip at Barnacle Bay",
                    ItemDescription = "Enjoy an overnight trip, fishing barnacle bombs from Barnacle Bay! Winning this package will give your heart a warm feeling while your nose dies in disgust.",
                    ItemValue = 80.00,
                    OpeningBid = 70.00,
                    BidIncrement = 10.00,
                    Auction = context.Auctions.First(a => a.AuctionId == 2),
                    Sponsor = context.Sponsors.First(s => s.sponsorID == 2),
					Category = context.Categories.First(c => c.CategoryId == 1),
					//Media = null,
					BidHistory = null
                },
                new Item
                {
                    ItemName = "Crazy Edd's Grand Fishing Day",
                    ItemDescription = "Crazy Edd has offered to take you on the grandest fishing trip ever. It will exceed your imagination's attempts at imagining it. Don't pass up this once in a lifetime opportunity!",
                    ItemValue = 200.00,
                    OpeningBid = 180.00,
                    BidIncrement = 10.00,
                    Auction = context.Auctions.First(a => a.AuctionId == 3),
                    Sponsor = context.Sponsors.First(s => s.sponsorID == 1),
					Category = context.Categories.First(c => c.CategoryId == 2),
					//Media = null,
					BidHistory = null
                },
                new Item
                {
                    ItemName = "Video games by Tom Hanks",
                    ItemDescription = "Tom Hanks will hand deliver three video games (of your choice) to you to own forever.",
                    ItemValue = 1200.00,
                    OpeningBid = 1000.00,
                    BidIncrement = 50.00,
                    Auction = context.Auctions.First(a => a.AuctionId == 2),
                    Sponsor = context.Sponsors.First(s => s.sponsorID == 3),
					Category = context.Categories.First(c => c.CategoryId == 3),
					//Media = null,
					BidHistory = null
                }
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
                    BidderId = 1
					//Auction = context.Auctions.First(a => a.AuctionId == 1),
					//Bidder = context.Bidders.First(b => b.BidderID == 1)
				},
                new BidHistory
                {
                    BidDate = DateTime.Parse("2017-7-10"),
                    BidAmount = 220.00,
                    ItemId = 3,
                    BidderId = 3
					//Auction = context.Auctions.First(a => a.AuctionId == 2),
					//Bidder = context.Bidders.First(b => b.BidderID == 1)
				},
                new BidHistory
                {
                    BidDate = DateTime.Parse("2017-7-11"),
                    BidAmount = 230.00,
                    ItemId = 3,
                    BidderId = 3
					//Auction = context.Auctions.First(a => a.AuctionId == 2),
					//Bidder = context.Bidders.First(b => b.BidderID == 2)
				},
                new BidHistory
                {
                    BidDate = DateTime.Parse("2017-7-12"),
                    BidAmount = 100.00,
                    ItemId = 2,
                    BidderId = 2
					//Auction = context.Auctions.First(a => a.AuctionId == 1),
					//Bidder = context.Bidders.First(b => b.BidderID == 2)
				},
                new BidHistory
                {
                    BidDate = DateTime.Parse("2017-7-13"),
                    BidAmount = 115.00,
                    ItemId = 2,
                    BidderId = 1
					//Auction = context.Auctions.First(a => a.AuctionId == 2),
					//Bidder = context.Bidders.First(b => b.BidderID == 3)
				},
                new BidHistory
                {
                    BidDate = DateTime.Parse("2017-7-12"),
                    BidAmount = 110.00,
                    ItemId = 1,
                    BidderId = 3
					//Auction = context.Auctions.First(a => a.AuctionId == 1),
					//Bidder = context.Bidders.First(b => b.BidderID == 3)
				},
                new BidHistory
                {
                    BidDate = DateTime.Parse("2017-7-14"),
                    BidAmount = 180.00,
                    ItemId = 1,
                    BidderId = 1
					//Auction = context.Auctions.First(a => a.AuctionId == 1),
					//Bidder = context.Bidders.First(b => b.BidderID == 1)
				},
            };

            foreach (BidHistory b in bids)
            {
                context.BidHistory.Add(b);
            }
            context.SaveChanges();


            var pics = new Media[]
                {
                    //carousel pic
                    new Media
                    {
                        ItemId = 0,
                        MediaName = "thanks.jpg",
                        MediaPath = "\\media\\carousel\\thanks.jpg"
                    },

                    new Media
                    {
                        ItemId = 0,
                        MediaName = "batman.jpg",
                        MediaPath = "\\media\\carousel\\batman.jpg"
                    },

                    new Media
                    {
                        ItemId = 0,
                        MediaName = "csharp.png",
                        MediaPath = "\\media\\carousel\\csharp.png"
                    },

                    new Media
                    {
                        ItemId = 0,
                        MediaName = "Nettle.png",
                        MediaPath = "\\media\\carousel\\Nettle.png"
                    },

                    new Media
                    {
                        ItemId = 0,
                        MediaName = "penguin.jpg",
                        MediaPath = "\\media\\carousel\\penguin.jpg"
                    },

                    new Media
                    {
                        ItemId = 0,
                        MediaName = "penguin2.jpg",
                        MediaPath = "\\media\\carousel\\penguin2.jpg"
                    },

                    new Media
                    {
                        ItemId = 0,
                        MediaName = "wsu.png",
                        MediaPath = "\\media\\carousel\\wsu.png"
                    },

                    new Media
                    {
                        ItemId = 1,
                        MediaName = "BarnacleBay.jpg",
                        MediaPath = "\\media\\item\\BarnacleBay.jpg"
                    },

                    new Media
                    {
                        ItemId = 2,
                        MediaName = "fishingTrip.jpg",
                         MediaPath = "\\media\\item\\fishingTrip.jpg"
                     },

                    new Media
                    {
                        ItemId = 3,
                        MediaName = "puddle.jpg",
                         MediaPath = "\\media\\item\\puddle.jpg"
                     },

                    new Media
                    {
                        ItemId = 4,
                        MediaName = "game.jpg",
                         MediaPath = "\\media\\item\\game.jpg"
                    }
             };

            foreach (Media m in pics)
            {
                context.Media.Add(m);
            }
            context.SaveChanges();
        }
    }
}

