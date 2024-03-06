using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class MusicAppContext : DbContext
    {
        public string DbPath { get; }

        public MusicAppContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@$"Data Source=(LocalDb)\MSSQLLocalDB;initial catalog=Backend_DAL.Models.MusicAppContext;integrated security=True;MultipleActiveResultSets=True;App=Backend_Dal");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordingPlaylist>()
                .HasOne(rp => rp.Creator)
                .WithMany(u => u.CreatedPlaylists)
                .HasForeignKey(rp => rp.creatorId)
                .OnDelete(DeleteBehavior.Restrict); // or use DeleteBehavior.Cascade if appropriate
           
            modelBuilder.Entity<User>()
                .HasMany(rp => rp.RequestsSendt)
                .WithOne(u=>u.firstUser)
                .HasForeignKey(rp => rp.firstUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(rp => rp.RequestReceived)
                .WithOne(u => u.secondUser)
                .HasForeignKey(rp => rp.secondUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RecordingPlaylist>()
                .HasMany(rp => rp.Guests)
                .WithMany(u => u.RecordingGuests);
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }
        public DbSet<RecordingPlaylist> Recordings { get; set; }
        public DbSet<Show> Shows {  get; set; }
        public DbSet<Show_song_played> Show_Song_Playeds { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_contact> User_Contacts { get; set; }

    }
}
