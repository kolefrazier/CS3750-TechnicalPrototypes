using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class addingUserBidhistoryView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidHistory_Auction_AuctionId",
                table: "BidHistory");

            migrationBuilder.DropIndex(
                name: "IX_BidHistory_AuctionId",
                table: "BidHistory");

            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "BidHistory");

            migrationBuilder.AddColumn<int>(
                name: "BidHistoryId",
                table: "Item",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Item_BidHistoryId",
                table: "Item",
                column: "BidHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_BidHistory_BidHistoryId",
                table: "Item",
                column: "BidHistoryId",
                principalTable: "BidHistory",
                principalColumn: "BidHistoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_BidHistory_BidHistoryId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_BidHistoryId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "BidHistoryId",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "AuctionId",
                table: "BidHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BidHistory_AuctionId",
                table: "BidHistory",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BidHistory_Auction_AuctionId",
                table: "BidHistory",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
