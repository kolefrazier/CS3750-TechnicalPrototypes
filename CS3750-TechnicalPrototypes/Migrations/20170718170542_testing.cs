using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS3750TechnicalPrototypes.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaTypeId",
                table: "Media",
                newName: "MediaTypeID");

            migrationBuilder.AlterColumn<int>(
                name: "MediaTypeID",
                table: "Media",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Media_MediaTypeID",
                table: "Media",
                column: "MediaTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_MediaType_MediaTypeID",
                table: "Media",
                column: "MediaTypeID",
                principalTable: "MediaType",
                principalColumn: "MediaTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_MediaType_MediaTypeID",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_MediaTypeID",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "MediaTypeID",
                table: "Media",
                newName: "MediaTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "MediaTypeId",
                table: "Media",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
