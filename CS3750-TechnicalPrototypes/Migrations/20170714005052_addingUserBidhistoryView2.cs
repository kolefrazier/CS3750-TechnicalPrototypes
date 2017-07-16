using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class addingUserBidhistoryView2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_BidHistory_BidHistoryId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "BidHistory");

            migrationBuilder.AlterColumn<int>(
                name: "BidHistoryId",
                table: "Item",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Item_BidHistory_BidHistoryId",
                table: "Item",
                column: "BidHistoryId",
                principalTable: "BidHistory",
                principalColumn: "BidHistoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_BidHistory_BidHistoryId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "BidHistoryId",
                table: "Item",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "BidHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_BidHistory_BidHistoryId",
                table: "Item",
                column: "BidHistoryId",
                principalTable: "BidHistory",
                principalColumn: "BidHistoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
