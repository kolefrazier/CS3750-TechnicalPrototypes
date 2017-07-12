using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CS3750TechnicalPrototypes.Models
{
	public class BidHistory
	{
		[Key]
		public int BidHistoryId { get; set; }
		public DateTime BidDate { get; set; }
		public double BidAmount { get; set; }

		//Navigation Properties
		public virtual int ItemId { get; set; }
		public virtual Auction Auction { get; set; }
	  //  public virtual Item Item { get; set; }
		public virtual Bidder Bidder { get; set; }

		public static implicit operator List<object>(BidHistory v)
		{
			throw new NotImplementedException();
		}
	}
}
