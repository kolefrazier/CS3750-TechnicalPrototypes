using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CS3750TechnicalPrototypes.Models
{
    public class BidHistory
    {
        [Key]
        public int BidHistoryId { get; set; }
        [Display(Name = "Bid Date")]
        public DateTime BidDate { get; set; }
        [Display(Name = "Bid Amount")]
        public double BidAmount { get; set; }


        //Navigation Properties
        // public int AuctionId { get; set; }
        public int ItemId { get; set; }
        public int BidderId { get; set; }
        public IEnumerable<Item> Item { get; set; }
        //public virtual Auction Auction { get; set; }
        //public virtual Bidder Bidder { get; set; }

        public static implicit operator List<object>(BidHistory v)
        {
            throw new NotImplementedException();
        }
    }
}
