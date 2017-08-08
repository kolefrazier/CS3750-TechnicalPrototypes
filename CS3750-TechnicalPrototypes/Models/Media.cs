using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CS3750TechnicalPrototypes.Models
{
    public class Media
    {
        [Key]
        public int PhotoID { get; set; }
        [Display(Name = "Media Path")]
        public string MediaPath { get; set; }
        [Display(Name = "Media Name")]
        public string MediaName { get; set; }
        [Display(Name = "Tooltip Text")]
        public string PhotoToolTip { get; set; }


        //Foreign Keys
        public int ItemId { get; set; }
        public MediaType MediaType { get; set; }

    }
}
