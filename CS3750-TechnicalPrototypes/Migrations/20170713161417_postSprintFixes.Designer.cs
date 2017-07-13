using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CS3750TechnicalPrototypes.Data;

namespace CS3750TechnicalPrototypes.Migrations
{
    [DbContext(typeof(AuctionContext))]
    [Migration("20170713161417_postSprintFixes")]
    partial class postSprintFixes
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

                    b.Property<DateTime>("StartDate");

                    b.HasKey("AuctionID");

                    b.ToTable("Auction");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.Bidder", b =>
                {
                    b.Property<int>("BidderID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsRegistered");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Security");

                    b.HasKey("BidderID");

                    b.ToTable("Bidder");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.BidHistory", b =>
                {
                    b.Property<int>("BidHistoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuctionID");

                    b.Property<double>("BidAmount");

                    b.Property<DateTime>("BidDate");

                    b.Property<int?>("BidderID");

                    b.Property<int>("ItemId");

                    b.HasKey("BidHistoryId");

                    b.HasIndex("AuctionID");

                    b.HasIndex("BidderID");

                    b.ToTable("BidHistory");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuctionID");

                    b.Property<DateTime>("closingEventDate");

                    b.Property<string>("eventDescription");

                    b.Property<string>("eventTitle");

                    b.Property<DateTime>("openingEventDate");

                    b.HasKey("EventID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuctionId");

                    b.Property<double>("BidIncrement");

                    b.Property<string>("ItemDescription");

                    b.Property<string>("ItemName");

                    b.Property<double>("ItemValue");

                    b.Property<double>("OpeningBid");

                    b.Property<int>("SponsorId");

                    b.HasKey("ItemId");

                    b.HasIndex("AuctionId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.BidHistory", b =>
                {
                    b.HasOne("CS3750TechnicalPrototypes.Models.Auction", "Auction")
                        .WithMany("BidHistory")
                        .HasForeignKey("AuctionID");

                    b.HasOne("CS3750TechnicalPrototypes.Models.Bidder", "Bidder")
                        .WithMany()
                        .HasForeignKey("BidderID");
                });

            modelBuilder.Entity("CS3750TechnicalPrototypes.Models.Item", b =>
                {
                    b.HasOne("CS3750TechnicalPrototypes.Models.Auction", "Auction")
                        .WithMany("Item")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
