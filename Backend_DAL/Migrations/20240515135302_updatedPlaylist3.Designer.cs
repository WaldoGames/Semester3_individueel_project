﻿// <auto-generated />
using System;
using Backend_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend_DAL.Migrations
{
    [DbContext(typeof(MusicAppContext))]
    [Migration("20240515135302_updatedPlaylist3")]
    partial class updatedPlaylist3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtistSong", b =>
                {
                    b.Property<int>("ArtistsId")
                        .HasColumnType("int");

                    b.Property<int>("SongsId")
                        .HasColumnType("int");

                    b.HasKey("ArtistsId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("ArtistSong");
                });

            modelBuilder.Entity("Backend_DAL.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("Backend_DAL.Models.PlaylistItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("discription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("orderIndex")
                        .HasColumnType("int");

                    b.Property<int>("playlistId")
                        .HasColumnType("int");

                    b.Property<int?>("playlistItemSongId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("playlistId");

                    b.HasIndex("playlistItemSongId");

                    b.ToTable("PlaylistItems");
                });

            modelBuilder.Entity("Backend_DAL.Models.RecordingPlaylist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("recordingPlayListName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recordings");
                });

            modelBuilder.Entity("Backend_DAL.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("show_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("show_language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("show_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("Backend_DAL.Models.Show_song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShowId");

                    b.HasIndex("SongId");

                    b.ToTable("Show_Song");
                });

            modelBuilder.Entity("Backend_DAL.Models.Show_song_played", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("showId")
                        .HasColumnType("int");

                    b.Property<int>("songId")
                        .HasColumnType("int");

                    b.Property<DateTime>("timePlayed")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("showId");

                    b.HasIndex("songId");

                    b.ToTable("Show_Song_Playeds");
                });

            modelBuilder.Entity("Backend_DAL.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Release_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("Backend_DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("auth0sub")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Backend_DAL.Models.User_contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("firstUserId")
                        .HasColumnType("int");

                    b.Property<int>("secondUserAcceptedRequest")
                        .HasColumnType("int");

                    b.Property<int>("secondUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("firstUserId");

                    b.HasIndex("secondUserId");

                    b.ToTable("User_Contacts");
                });

            modelBuilder.Entity("ShowUser", b =>
                {
                    b.Property<int>("ShowsId")
                        .HasColumnType("int");

                    b.Property<int>("hostsId")
                        .HasColumnType("int");

                    b.HasKey("ShowsId", "hostsId");

                    b.HasIndex("hostsId");

                    b.ToTable("ShowUser");
                });

            modelBuilder.Entity("ArtistSong", b =>
                {
                    b.HasOne("Backend_DAL.Models.Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend_DAL.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend_DAL.Models.PlaylistItem", b =>
                {
                    b.HasOne("Backend_DAL.Models.RecordingPlaylist", "playlist")
                        .WithMany("PlaylistItems")
                        .HasForeignKey("playlistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend_DAL.Models.Song", "playlistItemSong")
                        .WithMany()
                        .HasForeignKey("playlistItemSongId");

                    b.Navigation("playlist");

                    b.Navigation("playlistItemSong");
                });

            modelBuilder.Entity("Backend_DAL.Models.RecordingPlaylist", b =>
                {
                    b.HasOne("Backend_DAL.Models.User", "User")
                        .WithMany("CreatedPlaylists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend_DAL.Models.Show_song", b =>
                {
                    b.HasOne("Backend_DAL.Models.Show", "Show")
                        .WithMany("Songs")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend_DAL.Models.Song", "Song")
                        .WithMany("Shows")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Show");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("Backend_DAL.Models.Show_song_played", b =>
                {
                    b.HasOne("Backend_DAL.Models.Show", "show")
                        .WithMany("show_Songs")
                        .HasForeignKey("showId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend_DAL.Models.Song", "song")
                        .WithMany("SongsPlayed")
                        .HasForeignKey("songId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("show");

                    b.Navigation("song");
                });

            modelBuilder.Entity("Backend_DAL.Models.User_contact", b =>
                {
                    b.HasOne("Backend_DAL.Models.User", "firstUser")
                        .WithMany("RequestsSendt")
                        .HasForeignKey("firstUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Backend_DAL.Models.User", "secondUser")
                        .WithMany("RequestReceived")
                        .HasForeignKey("secondUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("firstUser");

                    b.Navigation("secondUser");
                });

            modelBuilder.Entity("ShowUser", b =>
                {
                    b.HasOne("Backend_DAL.Models.Show", null)
                        .WithMany()
                        .HasForeignKey("ShowsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend_DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("hostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend_DAL.Models.RecordingPlaylist", b =>
                {
                    b.Navigation("PlaylistItems");
                });

            modelBuilder.Entity("Backend_DAL.Models.Show", b =>
                {
                    b.Navigation("Songs");

                    b.Navigation("show_Songs");
                });

            modelBuilder.Entity("Backend_DAL.Models.Song", b =>
                {
                    b.Navigation("Shows");

                    b.Navigation("SongsPlayed");
                });

            modelBuilder.Entity("Backend_DAL.Models.User", b =>
                {
                    b.Navigation("CreatedPlaylists");

                    b.Navigation("RequestReceived");

                    b.Navigation("RequestsSendt");
                });
#pragma warning restore 612, 618
        }
    }
}
