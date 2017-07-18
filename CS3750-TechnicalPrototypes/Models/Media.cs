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
        public int MediaTypeId { get; set; }    
        public MediaType MediaType { get; set; }
        public int ItemID { get; set; }





    }
}
