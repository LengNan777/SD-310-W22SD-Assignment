using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations
{
    public partial class Initial : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArtistID = table.Column<int>(type: "int", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Song_Artist",
                        column: x => x.ArtistID,
                        principalTable: "Artist",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistID = table.Column<int>(type: "int", nullable: false),
                    SongID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Collection_Artist",
                        column: x => x.ArtistID,
                        principalTable: "Artist",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Collection_Song",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collection_ArtistID",
                table: "Collection",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_SongID",
                table: "Collection",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_Song_ArtistID",
                table: "Song",
                column: "ArtistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "Artist");
        }
    }
}
