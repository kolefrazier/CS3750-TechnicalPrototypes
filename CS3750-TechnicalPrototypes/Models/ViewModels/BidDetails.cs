using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS3750TechnicalPrototypes.Models;

namespace CS3750TechnicalPrototypes.Models.ViewModels
{
    public class BidDetails
    {
		public BidHistory BidHistory { get; set; }
		public Bidder Bidder { get; set; }
		public Item Item { get; set; }
	}
}
