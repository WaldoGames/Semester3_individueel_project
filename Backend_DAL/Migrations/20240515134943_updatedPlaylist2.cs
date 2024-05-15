using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedPlaylist2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recordings_Users_UserId",
                table: "Recordings");

            migrationBuilder.DropForeignKey(
                name: "FK_Recordings_Users_creatorId",
                table: "Recordings");

            migrationBuilder.DropIndex(
                name: "IX_Recordings_creatorId",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "creatorId",
                table: "Recordings");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Recordings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_Recordings_Users_UserId",
                table: "Recordings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recordings_Users_UserId1",
                table: "Recordings",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recordings_Users_UserId",
                table: "Recordings");

            migrationBuilder.DropForeignKey(
                name: "FK_Recordings_Users_UserId1",
                table: "Recordings");

            migrationBuilder.DropIndex(
                name: "IX_Recordings_UserId1",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Recordings");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Recordings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "creatorId",
                table: "Recordings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recordings_creatorId",
                table: "Recordings",
                column: "creatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recordings_Users_UserId",
                table: "Recordings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recordings_Users_creatorId",
                table: "Recordings",
                column: "creatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
