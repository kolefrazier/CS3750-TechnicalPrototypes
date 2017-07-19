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
		public DbSet<Bidder> Bidders { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
		public DbSet<Role> Roles { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }

		/// <summary>
		/// Removes plurality from DbSet property names when creating the table.
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Auction>().ToTable("Auction");
			modelBuilder.Entity<Item>().ToTable("Item");
			modelBuilder.Entity<BidHistory>().ToTable("BidHistory");
			modelBuilder.Entity<Bidder>().ToTable("Bidder");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Media>().ToTable("Media");
            modelBuilder.Entity<MediaType>().ToTable("MediaType");
			modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Sponsor>().ToTable("Sponsor");
        }
	}
}
