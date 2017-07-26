using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class sponsorMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sponsorID",
                table: "Media",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_sponsorID",
                table: "Media",
                column: "sponsorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Sponsor_sponsorID",
                table: "Media",
                column: "sponsorID",
                principalTable: "Sponsor",
                principalColumn: "sponsorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Sponsor_sponsorID",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_sponsorID",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "sponsorID",
                table: "Media");
        }
    }
}
