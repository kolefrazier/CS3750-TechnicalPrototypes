using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models.ViewModels
{
    public class ItemMedia
    {
        public Item Item { get; set; }
        public IEnumerable<Media> Media { get; set; }
        public double highestBid { get; set; }
    }
}
