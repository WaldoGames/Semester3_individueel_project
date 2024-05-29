using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core_unit_tests.IntegrationTests
{
    public class SongIntergrationTests : IntergrationTestsBase
    {
        ISongRepository s;

        public SongIntergrationTests(): base()
        {
            s= new SongRepository(context);
        }

        [Fact]
        public void GetSongsUsedByShow_HappyFlow_Get2Songs()
        {
            Result<SongsDto> result = s.GetSongsUsedByShow(1);

            Assert.False(result.IsFailed);
            Assert.Equal(2, result.Data.Songs.Count);
        }
        [Fact]
        public void GetSongsUsedByShow_InvalidId_GetEmptyList()
        {
            Result<SongsDto> result = s.GetSongsUsedByShow(8888);

            Assert.Empty(result.Data.Songs);
            Assert.False(result.IsFailed);
        }

        [Fact]
        public void DoesSongExist_HappyFlow_True()
        {
            Result<bool> result = s.DoesSongExist(1);

            Assert.False(result.IsFailed);
            Assert.True(result.Data);
        }
        [Fact]
        public void DoesSongExist_InvalidId_False()
        {
            Result<bool> result = s.DoesSongExist(8888);

            Assert.False(result.IsFailed);
            Assert.False(result.Data);
        }
        [Fact]
        public void PostPlayedSong_happyflow_SongPlayAdded()
        {
            Assert.Empty(context.Show_Song_Playeds);

            s.PostPlayedSong(new PlaySongDto { showId = 1, songId = 1 });

            Assert.Single(context.Show_Song_Playeds);
        }
        [Fact]
        public void PostPlayedSong_InvalidIds_IsFailedErrorAndEmptyIds()
        {
            Assert.Empty(context.Show_Song_Playeds);
            SimpleResult result = s.PostPlayedSong(new PlaySongDto { showId = 1897, songId = 1987 });

            Assert.True(result.IsFailedError);
            Assert.Empty(context.Show_Song_Playeds);
        }

        [Fact]
        public void PostNewSong_happyflow_SongAdded()
        {
            int i = context.Songs.Count();

            Result<int> result = s.PostNewSong(new NewSongDto { CreatorIds = { 1 }, name="new song", Release_date = new DateTime(2002,2,20), showId=1, User_description="new song is cool i guess"  });

            Assert.False(result.IsFailed);
            Assert.Equal(i + 1, context.Songs.Count());
            Assert.Equal("new song", context.Songs.Where(s => s.Id == result.Data).First().name);
            

        }
        [Fact]
        public void PostNewSong_InvalidData_Error()
        {
            Result<int> result = s.PostNewSong(new NewSongDto { CreatorIds = { context.Artists.FirstOrDefault().Id }, showId = 1, User_description = "new song is cool i guess" });

            Assert.True(result.IsFailedError);
        }

        [Fact]
        public void AddSongToShow_happyflow_SongConnestionAdded()
        {
            int i = context.Show_Song.Count();

            SimpleResult result = s.AddSongToShow(new NewSongDto { User_description = "lol", showId = 1 }, 1);

            Assert.False(result.IsFailed);
            Assert.Equal(i + 1, context.Show_Song.Count());
            

        }
        [Fact]
        public void AddSongToShow_InvalidData_Error()
        {

            SimpleResult result = s.AddSongToShow(new NewSongDto { User_description = "lol", showId = 9081 }, 1);

            Assert.True(result.IsFailedError);
        }

        [Fact]
        public void UpdateSong_happyflow_song1GetsDiffrentName()
        {
            Assert.Equal("song1", context.Songs.Where(s => s.Id == 1).First().name);

            SimpleResult result = s.UpdateSong(new UpdateSongDto { Id = 1, name = "SongChanged" });

            Assert.False(result.IsFailed);
            Assert.Equal("SongChanged", context.Songs.Where(s=>s.Id==1).First().name);


        }
        [Fact]
        public void UpdateSong_InvalidData_Error()
        {

            SimpleResult result = s.UpdateSong(new UpdateSongDto { User_description = "lol", showId = 9081 });

            Assert.True(result.IsFailedError);
        }

        [Fact]
        public void GetSong_happyflow_GetSong1()
        {

            NullableResult<SongDto> result = s.GetSong(1);

            Assert.Equal("song1", result.Data.name);
            Assert.False(result.IsEmpty);

        }
        [Fact]
        public void GetSong_InvalidId_GetNothing()
        {
            NullableResult<SongDto> result = s.GetSong(18976);

            Assert.True(result.IsEmpty);
        }
    }
}
