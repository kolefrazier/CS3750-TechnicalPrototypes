using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class Event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuctionID = table.Column<int>(nullable: false),
                    closingEventDate = table.Column<DateTime>(nullable: false),
                    eventDescription = table.Column<string>(nullable: true),
                    eventTitle = table.Column<string>(nullable: true),
                    openingEventDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");
        }
    }
}
