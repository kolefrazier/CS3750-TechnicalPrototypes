using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models
{
    public class MediaType
    {
        public int MediaTypeID { get; set; } 
        public int MediaDescription { get; set; }

        public virtual Media Media { get; set; } //abstract class
    }
}
