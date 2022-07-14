using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations
{
    public partial class AddUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArtistID",
                table: "Song",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Song",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Song_ArtistID",
                table: "Song",
                newName: "IX_Song_ArtistId");

            migrationBuilder.RenameColumn(
                name: "SongID",
                table: "Collection",
                newName: "SongId");

            migrationBuilder.RenameColumn(
                name: "ArtistID",
                table: "Collection",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Collection",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_SongID",
                table: "Collection",
                newName: "IX_Collection_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_ArtistID",
                table: "Collection",
                newName: "IX_Collection_ArtistId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Artist",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Collection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collection_UserId",
                table: "Collection",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_Users",
                table: "Collection",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collection_Users",
                table: "Collection");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Collection_UserId",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Collection");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Song",
                newName: "ArtistID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Song",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Song_ArtistId",
                table: "Song",
                newName: "IX_Song_ArtistID");

            migrationBuilder.RenameColumn(
                name: "SongId",
                table: "Collection",
                newName: "SongID");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Collection",
                newName: "ArtistID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Collection",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_SongId",
                table: "Collection",
                newName: "IX_Collection_SongID");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_ArtistId",
                table: "Collection",
                newName: "IX_Collection_ArtistID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Artist",
                newName: "ID");
        }
    }
}
