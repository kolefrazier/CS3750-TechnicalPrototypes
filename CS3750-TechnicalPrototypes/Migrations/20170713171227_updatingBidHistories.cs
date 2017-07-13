using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class updatingBidHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidHistory_Auction_AuctionID",
                table: "BidHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_BidHistory_Bidder_BidderID",
                table: "BidHistory");

            migrationBuilder.DropIndex(
                name: "IX_BidHistory_BidderID",
                table: "BidHistory");

            migrationBuilder.RenameColumn(
                name: "BidderID",
                table: "BidHistory",
                newName: "BidderId");

            migrationBuilder.RenameColumn(
                name: "AuctionID",
                table: "BidHistory",
                newName: "AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_BidHistory_AuctionID",
                table: "BidHistory",
                newName: "IX_BidHistory_AuctionId");

            migrationBuilder.RenameColumn(
                name: "AuctionID",
                table: "Auction",
                newName: "AuctionId");

            migrationBuilder.AlterColumn<int>(
                name: "BidderId",
                table: "BidHistory",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BidHistory_Auction_AuctionId",
                table: "BidHistory",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidHistory_Auction_AuctionId",
                table: "BidHistory");

            migrationBuilder.RenameColumn(
                name: "BidderId",
                table: "BidHistory",
                newName: "BidderID");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "BidHistory",
                newName: "AuctionID");

            migrationBuilder.RenameIndex(
                name: "IX_BidHistory_AuctionId",
                table: "BidHistory",
                newName: "IX_BidHistory_AuctionID");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "Auction",
                newName: "AuctionID");

            migrationBuilder.AlterColumn<int>(
                name: "BidderID",
                table: "BidHistory",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_BidHistory_BidderID",
                table: "BidHistory",
                column: "BidderID");

            migrationBuilder.AddForeignKey(
                name: "FK_BidHistory_Auction_AuctionID",
                table: "BidHistory",
                column: "AuctionID",
                principalTable: "Auction",
                principalColumn: "AuctionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BidHistory_Bidder_BidderID",
                table: "BidHistory",
                column: "BidderID",
                principalTable: "Bidder",
                principalColumn: "BidderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
