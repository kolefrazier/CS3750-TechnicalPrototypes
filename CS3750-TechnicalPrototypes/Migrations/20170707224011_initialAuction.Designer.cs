using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CS3750TechnicalPrototypes.Data;

namespace CS3750TechnicalPrototypes.Migrations
{
    [DbContext(typeof(AuctionContext))]
    [Migration("20170707224011_initialAuction")]
    partial class initialAuction
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

                    b.Property<DateTime>("endDate");

                    b.Property<int>("increment");

                    b.Property<int>("openingBid");

                    b.Property<DateTime>("startDate");

                    b.HasKey("AuctionID");

                    b.ToTable("Auction");
                });
        }
    }
}
