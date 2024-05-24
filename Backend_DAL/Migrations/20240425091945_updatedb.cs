using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistSong_Artists_CreatorsId",
                table: "ArtistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistSong_Songs_songsId",
                table: "ArtistSong");

            migrationBuilder.RenameColumn(
                name: "songsId",
                table: "ArtistSong",
                newName: "SongsId");

            migrationBuilder.RenameColumn(
                name: "CreatorsId",
                table: "ArtistSong",
                newName: "ArtistsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistSong_songsId",
                table: "ArtistSong",
                newName: "IX_ArtistSong_SongsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistSong_Artists_ArtistsId",
                table: "ArtistSong",
                column: "ArtistsId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistSong_Songs_SongsId",
                table: "ArtistSong",
                column: "SongsId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistSong_Artists_ArtistsId",
                table: "ArtistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistSong_Songs_SongsId",
                table: "ArtistSong");

            migrationBuilder.RenameColumn(
                name: "SongsId",
                table: "ArtistSong",
                newName: "songsId");

            migrationBuilder.RenameColumn(
                name: "ArtistsId",
                table: "ArtistSong",
                newName: "CreatorsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistSong_SongsId",
                table: "ArtistSong",
                newName: "IX_ArtistSong_songsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistSong_Artists_CreatorsId",
                table: "ArtistSong",
                column: "CreatorsId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistSong_Songs_songsId",
                table: "ArtistSong",
                column: "songsId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
