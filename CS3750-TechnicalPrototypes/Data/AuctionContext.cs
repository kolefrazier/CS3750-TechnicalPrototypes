using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS3750TechnicalPrototypes.Models;

namespace CS3750TechnicalPrototypes.Data
{
	public class AuctionContext : DbContext
	{
		public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
		{

		}

		//Any database models need to be listed here as DbSet<type> properties.
		//	Be sure to add them to the method below, OnModelCreating().
		public DbSet<Auction> Auctions { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<BidHistory> BidHistory { get; set; }

		/// <summary>
		/// Removes plurality from DbSet property names when creating the table.
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Auction>().ToTable("Auction");
			modelBuilder.Entity<Item>().ToTable("Item");
			modelBuilder.Entity<BidHistory>().ToTable("BidHistory");
		}
	}
}
