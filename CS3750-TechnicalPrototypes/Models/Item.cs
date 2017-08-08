using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CS3750TechnicalPrototypes.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Item Description")]
        public string ItemDescription { get; set; }
        [Display(Name = "Value")]
        public double ItemValue { get; set; }
        [Display(Name = "Opening Bid")]
        public double OpeningBid { get; set; }
        [Display(Name = "Bid Increment")]
        public double BidIncrement { get; set; }

        //Navigation Properties
        public int AuctionId { get; set; }
        public virtual Auction Auction { get; set; }
        public virtual BidHistory BidHistory { get; set; }
       // public IEnumerable<Media> Media { get; set; } causing issues with media uploads
        public Sponsor Sponsor { get; set; }
		public Category Category { get; set; }
	}
}
