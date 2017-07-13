using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class postSprintFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Auction_AuctionID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "OpeningBid",
                table: "Auction");

            migrationBuilder.RenameColumn(
                name: "AuctionID",
                table: "Item",
                newName: "AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_AuctionID",
                table: "Item",
                newName: "IX_Item_AuctionId");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "Item",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Auction_AuctionId",
                table: "Item",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "AuctionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Auction_AuctionId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "Item",
                newName: "AuctionID");

            migrationBuilder.RenameIndex(
                name: "IX_Item_AuctionId",
                table: "Item",
                newName: "IX_Item_AuctionID");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionID",
                table: "Item",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<double>(
                name: "OpeningBid",
                table: "Auction",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Auction_AuctionID",
                table: "Item",
                column: "AuctionID",
                principalTable: "Auction",
                principalColumn: "AuctionID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
