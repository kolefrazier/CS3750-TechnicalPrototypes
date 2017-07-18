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
        public string sponsorName { get; set; }

    }
}
