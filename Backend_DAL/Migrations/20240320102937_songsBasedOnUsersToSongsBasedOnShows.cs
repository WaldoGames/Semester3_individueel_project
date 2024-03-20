using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_DAL.Migrations
{
    /// <inheritdoc />
    public partial class songsBasedOnUsersToSongsBasedOnShows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Song_Users_UserId",
                table: "User_Song");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Song_Shows_UserId",
                table: "User_Song",
                column: "UserId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Song_Shows_UserId",
                table: "User_Song");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Song_Users_UserId",
                table: "User_Song",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
