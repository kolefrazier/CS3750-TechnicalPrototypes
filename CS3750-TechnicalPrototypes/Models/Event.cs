using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CS3750TechnicalPrototypes.Models
{
    public class Event
    {
            [Key]
            public int EventID { get; set; }
            public int AuctionID { get; set; }
            public string eventTitle { get; set; }
            public DateTime openingEventDate { get; set; }
            public DateTime closingEventDate { get; set; }
            public string eventDescription { get; set; }
    }
}
