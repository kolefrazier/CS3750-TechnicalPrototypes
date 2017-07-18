using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models
{
	public enum Roles
	{
		User, OfficeWorker, Administrator
	}

	public class Role
    {
		public int RoleID { get; set; }
		public string ShortDescription { get; set; }
		public Roles UserRole { get; set; }		
	}
}
