using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations
{
    public partial class addOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collection_Artist",
                table: "Collection");

            migrationBuilder.DropIndex(
                name: "IX_Collection_ArtistId",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Collection");

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Song",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Song",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sales",
                table: "Song",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Song",
                        column: x => x.SongId,
                        principalTable: "Song",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_SongId",
                table: "Order",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "Sales",
                table: "Song");

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Collection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Collection_ArtistId",
                table: "Collection",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_Artist",
                table: "Collection",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id");
        }
    }
}
