using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedPlaylist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordingPlaylistUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Recordings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "orderIndex",
                table: "PlaylistItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recordings_UserId",
                table: "Recordings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recordings_Users_UserId",
                table: "Recordings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recordings_Users_UserId",
                table: "Recordings");

            migrationBuilder.DropIndex(
                name: "IX_Recordings_UserId",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recordings");

            migrationBuilder.DropColumn(
                name: "orderIndex",
                table: "PlaylistItems");

            migrationBuilder.CreateTable(
                name: "RecordingPlaylistUser",
                columns: table => new
                {
                    GuestsId = table.Column<int>(type: "int", nullable: false),
                    RecordingGuestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordingPlaylistUser", x => new { x.GuestsId, x.RecordingGuestsId });
                    table.ForeignKey(
                        name: "FK_RecordingPlaylistUser_Recordings_RecordingGuestsId",
                        column: x => x.RecordingGuestsId,
                        principalTable: "Recordings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordingPlaylistUser_Users_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordingPlaylistUser_RecordingGuestsId",
                table: "RecordingPlaylistUser",
                column: "RecordingGuestsId");
        }
    }
}
