using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class fix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sponsorID",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_sponsorID",
                table: "Item",
                column: "sponsorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Sponsor_sponsorID",
                table: "Item",
                column: "sponsorID",
                principalTable: "Sponsor",
                principalColumn: "sponsorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Sponsor_sponsorID",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_sponsorID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "sponsorID",
                table: "Item");
        }
    }
}
