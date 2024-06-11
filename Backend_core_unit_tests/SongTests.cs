using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Classes;
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
        private IPlaylistRepository playlistRepository;
        public SongTests()
        {
            TestVar.test = true;
            songRepository = A.Fake<ISongRepository>();
            showRepository = A.Fake<IShowRepository>();
            artistRepository = A.Fake<IArtistRepository>();
            playlistRepository = A.Fake<IPlaylistRepository>();
            service = new SongService(showRepository, songRepository, artistRepository, playlistRepository);
        }

        [Fact]
        public void  GetSongsUsedInShow_happyFlow_SongCountIs2()
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
                            Artists = new List<ArtistDto> { new ArtistDto { Id = 2, name = "bob" }, doubleArtist } },

                                new SongWithLastPlayedDto { Id = 0, Release_date = DateTime.Now, name = "song2guess",
                            Artists = new List<ArtistDto> { new ArtistDto { Id = 2, name = "henk" }, doubleArtist } }
                        }
                    }
                }
           );

            Result<SongsDto> result= service.GetSongsUsedInShow(showId);

            Assert.False(result.IsFailed);
            Assert.True(result.Data.Songs.Count == 2);
        }

        [Fact]
        public void GetSongsUsedInShow_ShowNotFound_ResultIsFailed()
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
                            Artists = new List<ArtistDto> { new ArtistDto { Id = 2, name = "bob" }, doubleArtist } },

                                new SongWithLastPlayedDto { Id = 0, Release_date = DateTime.Now, name = "song2guess",
                            Artists = new List<ArtistDto> { new ArtistDto { Id = 2, name = "henk" }, doubleArtist } }
                        }

                    }
                }

           );

            Result<SongsDto> result = service.GetSongsUsedInShow(showId);

            Assert.True(result.IsFailed);
            
        }

        [Fact]
        public void GetSongsUsedInShow_emptyList_ResultDoesntFailButSongListIsEmpty()
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

        public void PlaySongOnShow_HappyFlow_ResultIsFailed()
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
        public void PlaySongOnShow_NoShow_AWarningIsReturnedAboutShows()
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
        public void PlaySongOnShow_NoSong_AWarningIsReturnedAboutSong()
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

        [Fact]
        public void PostSong_HappyFlow_ResultIsNotFailed()
        {
            A.CallTo(() => artistRepository.DoesArtistExist(A<int>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );
            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );
            A.CallTo(() => songRepository.PostNewSong(A<NewSongDto>._)).Returns(
               new Result<int>
               {
                   Data = 1
               }
            );
            A.CallTo(() => songRepository.AddSongToShow(A<NewSongDto>._, A<int>._)).Returns(
               new SimpleResult()
            ); //songRepository.PostNewSong(newSongDto);

            SimpleResult result= service.PostNewSong(new NewSongDto() { CreatorIds = new List<int> { 1, 2, 3 }, name = "ye", Release_date = new DateTime(2000, 1, 1), showId = 0, User_description = "meh" });

            Assert.False(result.IsFailed);
        }

        [Fact]
        public void PostSong_Noshow_AWarningIsReturnedAboutShows()
        {
            A.CallTo(() => artistRepository.DoesArtistExist(A<int>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );
            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
               new Result<bool>
               {
                   Data = false,
               }
            );
            A.CallTo(() => songRepository.PostNewSong(A<NewSongDto>._)).Returns(
               new Result<int>
               {
                   Data = 1
               }
            );
            A.CallTo(() => songRepository.AddSongToShow(A<NewSongDto>._, A<int>._)).Returns(
               new SimpleResult()
            ); //songRepository.PostNewSong(newSongDto);

            SimpleResult result = service.PostNewSong(new NewSongDto() { CreatorIds = new List<int> { 1, 2, 3 }, name = "ye", Release_date = new DateTime(2000, 1, 1), showId = 0, User_description = "meh" });

            Assert.True(result.IsFailedWarning);
            Assert.Contains("can't find your show", result.WarningMessage);
            Assert.False(result.IsFailedError);
        }

        [Fact]
        public void PostSong_ErrorShow_ShowDoesNotExistResultFailed()
        {
            A.CallTo(() => artistRepository.DoesArtistExist(A<int>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );
            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
               new Result<bool>
               {
                   ErrorMessage = "error",
               }
            );
            A.CallTo(() => songRepository.PostNewSong(A<NewSongDto>._)).Returns(
               new Result<int>
               {
                   Data = 1
               }
            );
            A.CallTo(() => songRepository.AddSongToShow(A<NewSongDto>._, A<int>._)).Returns(
               new SimpleResult()
            ); //songRepository.PostNewSong(newSongDto);

            SimpleResult result = service.PostNewSong(new NewSongDto() { CreatorIds = new List<int> { 1, 2, 3 }, name = "ye", Release_date = new DateTime(2000, 1, 1), showId = 0, User_description = "meh" });

            Assert.True(result.IsFailedError);
            Assert.False(result.IsFailedWarning);
        }

        [Fact]
        public void PostSong_NoAritist_ResultWarningAboutArtist()
        {
            A.CallTo(() => artistRepository.DoesArtistExist(A<int>._)).Returns(
               new Result<bool>
               {
                   Data = false,
               }
            );
            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );
            A.CallTo(() => songRepository.PostNewSong(A<NewSongDto>._)).Returns(
               new Result<int>
               {
                   Data = 1
               }
            );
            A.CallTo(() => songRepository.AddSongToShow(A<NewSongDto>._, A<int>._)).Returns(
               new SimpleResult()
            ); //songRepository.PostNewSong(newSongDto);

            SimpleResult result = service.PostNewSong(new NewSongDto() { CreatorIds = new List<int> { 1, 2, 3 }, name = "ye", Release_date = new DateTime(2000, 1, 1), showId = 0, User_description = "meh" });

            Assert.True(result.IsFailedWarning);
            Assert.Contains("one or more artist could not be added to the song", result.WarningMessage);
            Assert.False(result.IsFailedError);
        }

        [Fact]
        public void PostSong_ErrorAritist_ResultErrorAboutArtist()
        {
            A.CallTo(() => artistRepository.DoesArtistExist(A<int>._)).Returns(
               new Result<bool>
               {
                   
                   ErrorMessage = "error",
               }
            );
            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );
            A.CallTo(() => songRepository.PostNewSong(A<NewSongDto>._)).Returns(
               new Result<int>
               {
                   Data = 1
               }
            );
            A.CallTo(() => songRepository.AddSongToShow(A<NewSongDto>._, A<int>._)).Returns(
               new SimpleResult()
            ); //songRepository.PostNewSong(newSongDto);

            SimpleResult result = service.PostNewSong(new NewSongDto() { CreatorIds = new List<int> { 1, 2, 3 }, name = "ye", Release_date = new DateTime(2000, 1, 1), showId = 0, User_description = "meh" });

            Assert.True(result.IsFailedError);
            Assert.False(result.IsFailedWarning);
        }
        [Fact]
        public void PostSong_Warning_ResultWarningNoError()
        {
            A.CallTo(() => artistRepository.DoesArtistExist(A<int>._)).Returns(
               new Result<bool>
               {

                   Data = true,
               }
            );
            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );
            A.CallTo(() => songRepository.PostNewSong(A<NewSongDto>._)).Returns(
               new Result<int>
               {
                   WarningMessage = "warning"
               }
            );
            A.CallTo(() => songRepository.AddSongToShow(A<NewSongDto>._, A<int>._)).Returns(
               new SimpleResult()
            ); //songRepository.PostNewSong(newSongDto);

            SimpleResult result = service.PostNewSong(new NewSongDto() { CreatorIds = new List<int> { 1, 2, 3 }, name = "ye", Release_date = new DateTime(2000, 1, 1), showId = 0, User_description = "meh" });

            Assert.True(result.IsFailedWarning);
            Assert.False(result.IsFailedError);
        }

        [Fact]
        public void getSongSearch_happyflow_Get3Songs()
        {
            ArtistFactory af = new ArtistFactory();
            List<SongDto> songs = new List<SongDto>
            {
                new SongDto{ name="song1", Id=1, Release_date=DateTime.Now, Artists= new List<ArtistDto>{ af.GenerateFakeArtist("jeff") } },
                new SongDto{ name="song2", Id=2, Release_date=DateTime.Now, Artists= new List<ArtistDto>{ af.GenerateFakeArtist("jeff2") } },
                new SongDto{ name="song3", Id=3, Release_date=DateTime.Now, Artists= new List<ArtistDto>{ af.GenerateFakeArtist("jeff3") } }

            };
            SongsSimpleDto dto = new SongsSimpleDto
            {
                Songs = songs
            };

            A.CallTo(() => songRepository.GetSongsForSearch("song")).Returns(
                new Result<SongsSimpleDto>
                {
                    Data = dto,
                }
            );

            Result<SongsSimpleDto> result = service.getSongSearch("song");

            Assert.False(result.IsFailed);
            Assert.Equal(3, result.Data.Songs.Count);
        }
        [Fact]
        public void getSongSearch_NoSongfound_Get0Songs()
        {
            ArtistFactory af = new ArtistFactory();
            List<SongDto> songs = new List<SongDto>
            {

            };
            SongsSimpleDto dto = new SongsSimpleDto
            {
                Songs = songs
            };

            A.CallTo(() => songRepository.GetSongsForSearch("song")).Returns(
                new Result<SongsSimpleDto>
                {
                    Data = dto,
                }
            );

            Result<SongsSimpleDto> result = service.getSongSearch("song");

            Assert.False(result.IsFailed);
            Assert.Empty(result.Data.Songs);
        }
        [Fact]
        public void getSongSearch_Error_IsFailed()
        {
            ArtistFactory af = new ArtistFactory();
            List<SongDto> songs = new List<SongDto>
            {
                new SongDto{ name="song1", Id=1, Release_date=DateTime.Now, Artists= new List<ArtistDto>{ af.GenerateFakeArtist("jeff") } },
                new SongDto{ name="song2", Id=2, Release_date=DateTime.Now, Artists= new List<ArtistDto>{ af.GenerateFakeArtist("jeff2") } },
                new SongDto{ name="song3", Id=3, Release_date=DateTime.Now, Artists= new List<ArtistDto>{ af.GenerateFakeArtist("jeff3") } }

            };
            SongsSimpleDto dto = new SongsSimpleDto
            {
                Songs = songs
            };

            A.CallTo(() => songRepository.GetSongsForSearch("song")).Returns(
                new Result<SongsSimpleDto>
                {
                    ErrorMessage="IsFailed"
                }
            );

            Result<SongsSimpleDto> result = service.getSongSearch("song");

            Assert.True(result.IsFailed);
            
        }

        [Fact]
        public void AddInformationToSongDto_happyflow_GetSongWithShowConnectionUserDiscriptionDto()
        {
            ArtistFactory af = new ArtistFactory();



            SongDto newDto = new SongDto { 
                Id = 342, 
                Artists = [ af.GenerateFakeArtist("jeff") ], 
                name = "WEEEE", 
                Release_date = DateTime.Now };

            A.CallTo(() => showRepository.GetShowDiscriptionOfSong(A<int>._, A<int>._)).Returns(
                new NullableResult<string>
                {
                    Data = "added user discription",
                }
            );

            NullableResult<SongWithShowConnectionDto> result = service.AddInformationToSongDto(newDto, 1);

            Assert.False(result.IsFailed);
            Assert.Equal("WEEEE", result.Data.name);
            Assert.Equal("added user discription", result.Data.User_description);
        }

        [Fact]
        public void AddInformationToSongDto_NoDiscription_SongWithoutDiscription()
        {
            ArtistFactory af = new ArtistFactory();



            SongDto newDto = new SongDto
            {
                Id = 342,
                Artists = [af.GenerateFakeArtist("jeff")],
                name = "WEEEE",
                Release_date = DateTime.Now
            };

            A.CallTo(() => showRepository.GetShowDiscriptionOfSong(A<int>._, A<int>._)).Returns(
                new NullableResult<string>
                {
                
                }
            );

            NullableResult<SongWithShowConnectionDto> result = service.AddInformationToSongDto(newDto, 1);

            Assert.False(result.IsFailed);
            Assert.Equal("WEEEE", result.Data.name);
            Assert.Null(result.Data.User_description);
        }

        [Fact]
        public void AddInformationToSongDto_ErrorGetShowDiscriptionOfSong_IsFailed()
        {
            ArtistFactory af = new ArtistFactory();



            SongDto newDto = new SongDto
            {
                Id = 342,
                Artists = [af.GenerateFakeArtist("jeff")],
                name = "WEEEE",
                Release_date = DateTime.Now
            };

            A.CallTo(() => showRepository.GetShowDiscriptionOfSong(A<int>._, A<int>._)).Returns(
                new NullableResult<string>
                {
                    ErrorMessage="error"
                }
            );

            NullableResult<SongWithShowConnectionDto> result = service.AddInformationToSongDto(newDto, 1);

            Assert.False(result.IsFailedWarning);
            Assert.True(result.IsFailedError);
        }
    }
}
