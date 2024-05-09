using Backend_DAL.Classes;
using Backend_DAL.Models;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core_unit_tests.IntegrationTests
{
    public class IntergrationTestsBase
    {
        public MusicAppContext context;
        private DbContextOptions<MusicAppContext> options;

        public IntergrationTestsBase()
        {
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

            options = new DbContextOptionsBuilder<MusicAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .UseInternalServiceProvider(serviceProvider)
            .Options;

            context = new MusicAppContext(options);

            SeedTestData();
        }

        private void SeedTestData()
        {
            List<Artist> artists = new List<Artist> {

                new Artist { Id = 1, name = "jeff" },
                new Artist { Id = 2, name = "jeffry" },
                new Artist { Id = 3, name = "bob" },
                new Artist { Id = 4, name = "billy" },

               };
            List<Song> songs = new List<Song>
            {
                new Song { Id = 1, name = "song1", Release_date = DateTime.Now, Artists={ artists[0]} },
                new Song { Id = 2, name = "song2", Release_date = DateTime.Now, Artists={ artists[1], artists[0] } },
                new Song { Id = 3, name = "song3", Release_date = DateTime.Now, Artists={ artists[2]} },
            };
            List<Show> shows = new List<Show>
            {
                new Show{ Id=1 ,show_name="show1", show_description="show1", show_language="en"},
            };
            List<Show_song> show_Songs = new List<Show_song>
            {
                new Show_song{ Id=1, Information = "hello", Show = shows[0], Song = songs[0]},
                new Show_song{ Id=2, Information = "hello", Show = shows[0], Song = songs[1]}
            };

            context.Artists.AddRange(artists);
            context.Songs.AddRange(songs);
            context.Shows.AddRange(shows);
            context.Show_Song.AddRange(show_Songs);
            context.SaveChanges();
        }
    }
}
