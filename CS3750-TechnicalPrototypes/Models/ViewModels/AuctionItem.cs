using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models.ViewModels
{
    public class AuctionItem
    {
        public Auction Auction { get; set; }
        public IEnumerable<Item> Item { get; set; } 
        public IEnumerable<Media> Media { get; set; }
    }
}
