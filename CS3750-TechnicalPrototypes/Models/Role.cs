using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models
{
	public enum Roles
	{
        [Display(Name = "Regular User")]
		User,
        [Display(Name = "Office Staff")]
        OfficeWorker,
        [Display(Name = "Site Administrator")]
        Administrator
	}

	public class Role
    {
		public int RoleID { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Display(Name = "Role Level (enum)")]
		public Roles UserRole { get; set; }		
	}
}
