using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using FakeItEasy;
using FakeItEasy.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core_unit_tests
{
    public class SongTests
    {
        private SongService service;
        private ISongRepository songRepository;
        private IShowRepository showRepository;
        private IArtistRepository artistRepository;
        public SongTests()
        {
            TestVar.test = true;
            songRepository = A.Fake<ISongRepository>();
            showRepository = A.Fake<IShowRepository>();
            artistRepository = A.Fake<IArtistRepository>();
            service = new SongService(showRepository, songRepository, artistRepository);
        }

        [Fact]
        public void  GetSongsUsedInShow_happyFlow()
        {

            ArtistDto doubleArtist = new ArtistDto { Id = 1, name = "jeff" };
            int showId = 1;

            A.CallTo(() => showRepository.DoesShowExist(showId)).Returns(
                new Result<bool>
                {
                    Data = true,
                }
            );
            A.CallTo(() => songRepository.GetSongsUsedByShow(showId)).Returns(
                new Result<SongsDto>
                {
                    Data = new SongsDto
                    {
                        Songs = new List<SongWithLastPlayedDto> { new SongWithLastPlayedDto { Id = 0, Release_date = DateTime.Now, name = "songiguess",
                            Creators = new List<ArtistDto> { new ArtistDto { Id = 2, name = "bob" }, doubleArtist } },

                                new SongWithLastPlayedDto { Id = 0, Release_date = DateTime.Now, name = "song2guess",
                            Creators = new List<ArtistDto> { new ArtistDto { Id = 2, name = "henk" }, doubleArtist } }
                        }

                    }
                }

           );

            Result<SongsDto> result= service.GetSongsUsedInShow(showId);

            Assert.False(result.IsFailed);
            Assert.True(result.Data.Songs.Count == 2);
        }

        [Fact]
        public void GetSongsUsedInShow_ShowNotFound()
        {

            ArtistDto doubleArtist = new ArtistDto { Id = 1, name = "jeff" };
            int showId = 1;

            A.CallTo(() => showRepository.DoesShowExist(showId)).Returns(
                new Result<bool>
                {
                    WarningMessage = "show not found"
                }
            );
            A.CallTo(() => songRepository.GetSongsUsedByShow(showId)).Returns(
                new Result<SongsDto>
                {
                    Data = new SongsDto
                    {
                        Songs = new List<SongWithLastPlayedDto> { new SongWithLastPlayedDto { Id = 0, Release_date = DateTime.Now, name = "songiguess",
                            Creators = new List<ArtistDto> { new ArtistDto { Id = 2, name = "bob" }, doubleArtist } },

                                new SongWithLastPlayedDto { Id = 0, Release_date = DateTime.Now, name = "song2guess",
                            Creators = new List<ArtistDto> { new ArtistDto { Id = 2, name = "henk" }, doubleArtist } }
                        }

                    }
                }

           );

            Result<SongsDto> result = service.GetSongsUsedInShow(showId);

            Assert.True(result.IsFailed);
            
        }

        [Fact]
        public void GetSongsUsedInShow_emptyList()
        {

            ArtistDto doubleArtist = new ArtistDto { Id = 1, name = "jeff" };
            int showId = 1;

            A.CallTo(() => showRepository.DoesShowExist(showId)).Returns(
                new Result<bool>
                {
                    Data = true,
                }
            );
            A.CallTo(() => songRepository.GetSongsUsedByShow(showId)).Returns(
                new Result<SongsDto>
                {
                    Data = new SongsDto
                    {
                        Songs = new List<SongWithLastPlayedDto> {}
                        

                    }
                }

           );

            Result<SongsDto> result = service.GetSongsUsedInShow(showId);

            Assert.False(result.IsFailed);
            Assert.Empty(result.Data.Songs);

        }

        [Fact]

        public void PlaySongOnShow_HappyFlow()
        {

            int showId = 1;

            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
                new Result<bool>
                {
                    Data = true,
                }
            );

            A.CallTo(() => songRepository.DoesSongExist(A<int>._)).Returns(
                new Result<bool>
                {
                    Data = true,
                }
            );

            SimpleResult result = service.PostSongPlayed(new PlaySongDto { showId = 0, songId = 1, timePlayed = new DateTime(2020, 10, 20) });

            Assert.False(result.IsFailed);
        }
        [Fact]
        public void PlaySongOnShow_NoShow()
        {

            int showId = 1;

            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
                new Result<bool>
                {
                    Data = false,
                }
            );

            A.CallTo(() => songRepository.DoesSongExist(A<int>._)).Returns(
                new Result<bool>
                {
                    Data = true,
                }
            );

            SimpleResult result = service.PostSongPlayed(new PlaySongDto { showId = 1, songId = 1, timePlayed = new DateTime(2020, 10, 20) });

            Assert.Contains("show", result.WarningMessage);

            Assert.True(result.IsFailed);
        }
        [Fact]
        public void PlaySongOnShow_NoSong()
        {

            int showId = 1;

            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
                new Result<bool>
                {
                    Data = true,
                }
            );

            A.CallTo(() => songRepository.DoesSongExist(A<int>._)).Returns(
                new Result<bool>
                {
                    Data = false,
                }
            );

            SimpleResult result = service.PostSongPlayed(new PlaySongDto { showId = 0, songId = 1, timePlayed = new DateTime(2020, 10, 20) });

            Assert.Contains("song", result.WarningMessage);

            Assert.True(result.IsFailed);
        }
    }
}
