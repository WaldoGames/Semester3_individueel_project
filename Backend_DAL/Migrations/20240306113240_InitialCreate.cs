using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    show_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    show_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    show_language = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Release_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtistSong",
                columns: table => new
                {
                    CreatorsId = table.Column<int>(type: "int", nullable: false),
                    songsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSong", x => new { x.CreatorsId, x.songsId });
                    table.ForeignKey(
                        name: "FK_ArtistSong_Artists_CreatorsId",
                        column: x => x.CreatorsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistSong_Songs_songsId",
                        column: x => x.songsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Show_Song_Playeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timePlayed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    showId = table.Column<int>(type: "int", nullable: false),
                    songId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Show_Song_Playeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Show_Song_Playeds_Shows_showId",
                        column: x => x.showId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Show_Song_Playeds_Songs_songId",
                        column: x => x.songId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recordings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recordingPlayListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recordings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recordings_Users_creatorId",
                        column: x => x.creatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShowUser",
                columns: table => new
                {
                    ShowsId = table.Column<int>(type: "int", nullable: false),
                    hostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowUser", x => new { x.ShowsId, x.hostsId });
                    table.ForeignKey(
                        name: "FK_ShowUser_Shows_ShowsId",
                        column: x => x.ShowsId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowUser_Users_hostsId",
                        column: x => x.hostsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    secondUserAcceptedRequest = table.Column<int>(type: "int", nullable: false),
                    firstUserId = table.Column<int>(type: "int", nullable: false),
                    secondUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Contacts_Users_firstUserId",
                        column: x => x.firstUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Contacts_Users_secondUserId",
                        column: x => x.secondUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlaylistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    playlistId = table.Column<int>(type: "int", nullable: false),
                    playlistItemSongId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistItems_Recordings_playlistId",
                        column: x => x.playlistId,
                        principalTable: "Recordings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistItems_Songs_playlistItemSongId",
                        column: x => x.playlistItemSongId,
                        principalTable: "Songs",
                        principalColumn: "Id");
                });

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
                name: "IX_ArtistSong_songsId",
                table: "ArtistSong",
                column: "songsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistItems_playlistId",
                table: "PlaylistItems",
                column: "playlistId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistItems_playlistItemSongId",
                table: "PlaylistItems",
                column: "playlistItemSongId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordingPlaylistUser_RecordingGuestsId",
                table: "RecordingPlaylistUser",
                column: "RecordingGuestsId");

            migrationBuilder.CreateIndex(
                name: "IX_Recordings_creatorId",
                table: "Recordings",
                column: "creatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Show_Song_Playeds_showId",
                table: "Show_Song_Playeds",
                column: "showId");

            migrationBuilder.CreateIndex(
                name: "IX_Show_Song_Playeds_songId",
                table: "Show_Song_Playeds",
                column: "songId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowUser_hostsId",
                table: "ShowUser",
                column: "hostsId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Contacts_firstUserId",
                table: "User_Contacts",
                column: "firstUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Contacts_secondUserId",
                table: "User_Contacts",
                column: "secondUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistSong");

            migrationBuilder.DropTable(
                name: "PlaylistItems");

            migrationBuilder.DropTable(
                name: "RecordingPlaylistUser");

            migrationBuilder.DropTable(
                name: "Show_Song_Playeds");

            migrationBuilder.DropTable(
                name: "ShowUser");

            migrationBuilder.DropTable(
                name: "User_Contacts");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Recordings");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
