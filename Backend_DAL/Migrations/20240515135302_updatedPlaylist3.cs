using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedPlaylist3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recordings_Users_UserId1",
                table: "Recordings");

            migrationBuilder.DropIndex(
                name: "IX_Recordings_UserId1",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Recordings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Recordings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recordings_UserId1",
                table: "Recordings",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recordings_Users_UserId1",
                table: "Recordings",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
