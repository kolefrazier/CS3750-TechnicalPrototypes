using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class addmedia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaType",
                columns: table => new
                {
                    MediaTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MediaDescription = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaType", x => x.MediaTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    PhotoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemID = table.Column<int>(nullable: false),
                    MediaName = table.Column<string>(nullable: true),
                    MediaPath = table.Column<string>(nullable: true),
                    MediaTypeId = table.Column<int>(nullable: false),
                    PhotoToolTip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.PhotoID);
                    table.ForeignKey(
                        name: "FK_Media_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Media_MediaType_MediaTypeId",
                        column: x => x.MediaTypeId,
                        principalTable: "MediaType",
                        principalColumn: "MediaTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Media_ItemID",
                table: "Media",
                column: "ItemID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_MediaTypeId",
                table: "Media",
                column: "MediaTypeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "MediaType");
        }
    }
}
