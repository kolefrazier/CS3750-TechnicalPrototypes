using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models.ViewModels
{
	public class BidCreateViewModel
	{
		//BidHistory placeholders
		[Display(Name = "Bid Date", Description = "Defaults to today's date and time.")]
		public DateTime BidDate { get; set; }
		[Display(Name = "Bid Amount")]
		public double BidAmount { get; set; }
		public int ItemId { get; set; }
		public int AuctionId { get; set; }
        public Item Item { get; set; }
        public IEnumerable<Media> Media { get; set; }

		//Bidder model placeholders
		[Display(Name = "First Name")]
		public string Bidder_FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string Bidder_LastName { get; set; }
		[Display(Name = "Phone Number")]
		public string Bidder_PhoneNumber { get; set; }
		[Display(Name = "Email Address")]
		public string Bidder_EmailAddress { get; set; }
	}
}
