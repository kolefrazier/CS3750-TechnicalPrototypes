﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS3750TechnicalPrototypes.Models
{
	public class Bidder
	{
		public int BidderID { get; set; }

		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
		[Required]
		[DataType(DataType.PhoneNumber)]
		public string EmailAddress { get; set; }

		public bool IsRegistered { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }
		public string Security { get; set; }

		public Role Role { get; set; }
	}
}
