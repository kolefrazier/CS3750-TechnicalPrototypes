using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CS3750TechnicalPrototypes.Models
{
    public class Sponsor
    {
        [Key]
        public int sponsorID { get; set; }
        [Required]
        [Display(Name = "Sponsor Name")]
        public string sponsorName { get; set; }
        public string sponsorEmail { get; set; }
        public IEnumerable<Item> Item { get; set; }

        //public Media Media { get; set; }
    }
}
