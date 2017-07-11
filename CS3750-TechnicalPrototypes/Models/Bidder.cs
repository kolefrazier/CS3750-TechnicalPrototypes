using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models
{
    public class Bidder
    {
		public int BidderID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string EmailAddress { get; set; }
		public bool IsRegistered { get; set; }
		public string Password { get; set; }
		public string Security { get; set; }
	}
}
