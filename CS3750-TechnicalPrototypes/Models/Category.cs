using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models
{
    public class Category
    {
		public int CategoryId { get; set; }
		[Required]
		[Display(Name = "Category Name")]
		public string Name { get; set; }
		[Required]
		[Display(Name = "Category Description")]
		public string Description { get; set; }
	}
}
