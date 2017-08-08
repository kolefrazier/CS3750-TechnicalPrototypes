using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models
{
    public class MediaType
    {
        public int MediaTypeID { get; set; }
        [Display(Name = "Media Description")]
        public string MediaDescription { get; set; }

        //public virtual Media Media { get; set; } //abstract class
    }
}
