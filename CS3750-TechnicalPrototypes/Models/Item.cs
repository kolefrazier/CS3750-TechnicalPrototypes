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
        public int SponsorId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double ItemValue { get; set; }
        public double OpeningBid { get; set; }
        public double BidIncrement { get; set; }

        //Navigation Properties
        public int AuctionId { get; set; }
      //  public int BidHistoryId { get; set; }
        public virtual Auction Auction { get; set; }
        public virtual BidHistory BidHistory { get; set; }
        public virtual Media Media { get; set; }
    }
}
