using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_DAL.Migrations
{
    /// <inheritdoc />
    public partial class movedplaylisttoshowinsteadofuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recordings_Users_UserId",
                table: "Recordings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Recordings",
                newName: "ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_Recordings_UserId",
                table: "Recordings",
                newName: "IX_Recordings_ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recordings_Shows_ShowId",
                table: "Recordings",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recordings_Shows_ShowId",
                table: "Recordings");

            migrationBuilder.RenameColumn(
                name: "ShowId",
                table: "Recordings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Recordings_ShowId",
                table: "Recordings",
                newName: "IX_Recordings_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recordings_Users_UserId",
                table: "Recordings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
