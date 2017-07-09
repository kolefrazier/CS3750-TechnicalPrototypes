using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CS3750TechnicalPrototypes.Models
{

	public class Auction
	{
		[Key]
		public int AuctionID { get; set; }
		public string AuctionName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public double OpeningBid { get; set; }
		public int EventId { get; set; }

		//Navigation Properties
		public IEnumerable<Item> Item { get; set; }
		public IEnumerable<BidHistory> BidHistory { get; set; }
	}

}
