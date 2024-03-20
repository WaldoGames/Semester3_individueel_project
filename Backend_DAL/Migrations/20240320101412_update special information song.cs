using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatespecialinformationsong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "User_Song",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Song", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Song_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Song_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Song_SongId",
                table: "User_Song",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Song_UserId",
                table: "User_Song",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Song");

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
