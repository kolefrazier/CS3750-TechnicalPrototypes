using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models
{
    public class Auction
    {
        public int AuctionID { get; set; }
        public string AuctionName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int openingBid { get; set; }
        public int increment { get; set; }
        public string description { get; set; }
    }
}
