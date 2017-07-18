using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class mediaFixing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Media_ItemId",
                table: "Media");

            migrationBuilder.CreateIndex(
                name: "IX_Media_ItemId",
                table: "Media",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Media_ItemId",
                table: "Media");

            migrationBuilder.CreateIndex(
                name: "IX_Media_ItemId",
                table: "Media",
                column: "ItemId",
                unique: true);
        }
    }
}
