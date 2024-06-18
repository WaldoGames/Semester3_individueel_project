using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    public class MusicAppContext : DbContext
    {
        public MusicAppContext() : base()
        {

        }
        public MusicAppContext(DbContextOptions<MusicAppContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@$"Server=sql_server_container;Database=Backend_DAL;User Id=SA;Password=Pass-Sql-Word-14434-AAA#A;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");//, builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null); });
        //Server=sql_server_container;Database=Backend_DAL;User Id=SA;Password=Pass-Sql-Word-14434-AAA#A;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
        //localhost, 1433
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordingPlaylist>()
                .HasOne(rp => rp.Show)
                .WithMany(u => u.CreatedPlaylists)
                .OnDelete(DeleteBehavior.Restrict); // or use DeleteBehavior.Cascade if appropriate

            modelBuilder.Entity<User>()
                .HasMany(rp => rp.RequestsSendt)
                .WithOne(u => u.firstUser)
                .HasForeignKey(rp => rp.firstUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(rp => rp.RequestReceived)
                .WithOne(u => u.secondUser)
                .HasForeignKey(rp => rp.secondUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Song>()
            .HasMany(s=>s.SongsPlayed)
            .WithOne(u => u.song)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Show>()
            .HasMany(rp => rp.Songs)
            .WithOne(u => u.Show)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Show>()
            .HasMany(rp => rp.show_Songs)
            .WithOne(u => u.show)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Show>()
            .HasMany(rp => rp.CreatedPlaylists)
            .WithOne(u => u.Show)
            .OnDelete(DeleteBehavior.Cascade);
        }
                
        

        public DbSet<Artist> Artists { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }
        public DbSet<RecordingPlaylist> Recordings { get; set; }
        public DbSet<Show> Shows {  get; set; }
        public DbSet<Show_song_played> Show_Song_Playeds { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_contact> User_Contacts { get; set; }
        public DbSet<Show_song> Show_Song { get; set; }

    }
}
