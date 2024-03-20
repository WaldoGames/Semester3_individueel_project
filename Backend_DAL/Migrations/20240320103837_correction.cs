using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_DAL.Migrations
{
    /// <inheritdoc />
    public partial class correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Song_Shows_UserId",
                table: "User_Song");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Song_Songs_SongId",
                table: "User_Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Song",
                table: "User_Song");

            migrationBuilder.RenameTable(
                name: "User_Song",
                newName: "Show_Song");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Show_Song",
                newName: "ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Song_UserId",
                table: "Show_Song",
                newName: "IX_Show_Song_ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Song_SongId",
                table: "Show_Song",
                newName: "IX_Show_Song_SongId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Show_Song",
                table: "Show_Song",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Show_Song_Shows_ShowId",
                table: "Show_Song",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Show_Song_Songs_SongId",
                table: "Show_Song",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Show_Song_Shows_ShowId",
                table: "Show_Song");

            migrationBuilder.DropForeignKey(
                name: "FK_Show_Song_Songs_SongId",
                table: "Show_Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Show_Song",
                table: "Show_Song");

            migrationBuilder.RenameTable(
                name: "Show_Song",
                newName: "User_Song");

            migrationBuilder.RenameColumn(
                name: "ShowId",
                table: "User_Song",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Show_Song_SongId",
                table: "User_Song",
                newName: "IX_User_Song_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_Show_Song_ShowId",
                table: "User_Song",
                newName: "IX_User_Song_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Song",
                table: "User_Song",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Song_Shows_UserId",
                table: "User_Song",
                column: "UserId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Song_Songs_SongId",
                table: "User_Song",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
