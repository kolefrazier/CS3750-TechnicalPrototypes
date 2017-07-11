using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CS3750TechnicalPrototypes.Data;

namespace CS3750TechnicalPrototypes.Migrations
{
    [DbContext(typeof(AuctionContext))]
    [Migration("20170710031553_AddedItemIdToAuction")]
    partial class AddedItemIdToAuction
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.Auction", b =>
                {
                    b.Property<int>("AuctionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuctionName");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("EventId");

                    b.Property<int>("ItemID");

                    b.Property<double>("OpeningBid");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("AuctionID");

                    b.ToTable("Auction");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.BidHistory", b =>
                {
                    b.Property<int>("BidHistoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuctionID");

                    b.Property<double>("BidAmount");

                    b.Property<DateTime>("BidDate");

                    b.Property<int>("ItemId");

                    b.HasKey("BidHistoryId");

                    b.HasIndex("AuctionID");

                    b.ToTable("BidHistory");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuctionID");

                    b.Property<double>("BidIncrement");

                    b.Property<string>("ItemDescription");

                    b.Property<string>("ItemName");

                    b.Property<double>("ItemValue");

                    b.Property<double>("OpeningBid");

                    b.Property<int>("SponsorId");

                    b.HasKey("ItemId");

                    b.HasIndex("AuctionID");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.BidHistory", b =>
                {
                    b.HasOne("CS3750TechnicalPrototypes.Models.Auction", "Auction")
                        .WithMany("BidHistory")
                        .HasForeignKey("AuctionID");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.Item", b =>
                {
                    b.HasOne("CS3750TechnicalPrototypes.Models.Auction", "Auction")
                        .WithMany("Item")
                        .HasForeignKey("AuctionID");
                });
        }
    }
}
