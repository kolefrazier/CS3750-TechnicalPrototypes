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
        public string MediaPath { get; set; }
        public string MediaName { get; set; }
        public string PhotoToolTip { get; set; }

        //Foreign Keys
      //  public int MediaTypeId { get; set; }    
        public int ItemId { get; set; }
        //public int sponsorID { get; set; }
      //  public virtual Item Item { get; set; }
        public MediaType MediaType { get; set; }
        public Sponsor Sponsor { get; set; }
    }
}
