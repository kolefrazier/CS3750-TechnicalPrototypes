using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class BidderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BidderID",
                table: "BidHistory",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bidder",
                columns: table => new
                {
                    BidderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailAddress = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsRegistered = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Security = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bidder", x => x.BidderID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidHistory_BidderID",
                table: "BidHistory",
                column: "BidderID");

            migrationBuilder.AddForeignKey(
                name: "FK_BidHistory_Bidder_BidderID",
                table: "BidHistory",
                column: "BidderID",
                principalTable: "Bidder",
                principalColumn: "BidderID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidHistory_Bidder_BidderID",
                table: "BidHistory");

            migrationBuilder.DropTable(
                name: "Bidder");

            migrationBuilder.DropIndex(
                name: "IX_BidHistory_BidderID",
                table: "BidHistory");

            migrationBuilder.DropColumn(
                name: "BidderID",
                table: "BidHistory");
        }
    }
}
