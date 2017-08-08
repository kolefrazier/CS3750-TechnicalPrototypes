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
        [Display(Name = "Event Title")]
        public string eventTitle { get; set; }
        [Display(Name = "Opening Date")]
        public DateTime openingEventDate { get; set; }
        [Display(Name = "Closing Date")]
        public DateTime closingEventDate { get; set; }
        [Display(Name = "Event Description")]
        public string eventDescription { get; set; }

        public int AuctionID { get; set; }
    }
}
