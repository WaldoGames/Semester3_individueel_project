using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_core_unit_tests.Factory;
using Backend_DAL.Models;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;

namespace Backend_core_unit_tests
{
    public class ArtistTests
    {


        private ArtistService service;
        private IArtistRepository artistRepository;
        private ISongRepository songRepository;
        private IShowRepository showRepository;
        public ArtistTests()
        {
            TestVar.test = true;
            artistRepository = A.Fake<IArtistRepository>();
            songRepository = A.Fake<ISongRepository>();
            showRepository = A.Fake<IShowRepository>();

            service = new ArtistService(showRepository, songRepository, artistRepository);
        }

        [Fact]
        public void GetArtistPlayedCount_happyflow_OneArtistPLayedTwoTimes()
        {
            ArtistFactory af = new ArtistFactory();
            List<ArtistDto> artists = new List<ArtistDto>
            {
                af.GenerateFakeArtist("jeff"), af.GenerateFakeArtist("bob"),af.GenerateFakeArtist("steve"),
            };

            SongsDto songDto = new SongsDto()
            {
                Songs = new List<SongWithLastPlayedDto> {
                    new SongWithLastPlayedDto
                    {
                        Id = 1,
                        AmountPlayed = 1,
                        name = "1",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "1",
                        Artists = new List<ArtistDto>
                        {
                            artists[0]
                        }
                    },
                                        
                    new SongWithLastPlayedDto
                    {
                        Id = 2,
                        AmountPlayed = 1,
                        name = "2",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "2",
                        Artists = new List<ArtistDto>
                        {
                            artists[1],artists[0]
                        }
                    },
                                                            
                    new SongWithLastPlayedDto
                    {
                        Id = 3,
                        AmountPlayed = 1,
                        name = "3",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "3",
                        Artists = new List<ArtistDto>
                        {
                            artists[2]
                        }
                    },
                }
            };

            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
                new Result<bool>
                {
                    Data = true,
                }
            );
            A.CallTo(()=> songRepository.GetSongsUsedByShow(A<int>._)).Returns(
                new Result<SongsDto>
                {
                    Data = songDto,
                }
            );

            Result<int> result = service.GetArtistPlayedCount(1, 0);

            Assert.False(result.IsFailed);
            Assert.Equal(2, result.Data);
        }

        [Fact]
        public void GetArtistPlayedCount_happyflow_OneArtistPLayedOneTime()
        {
            ArtistFactory af = new ArtistFactory();
            List<ArtistDto> artists = new List<ArtistDto>
            {
                af.GenerateFakeArtist("jeff"), af.GenerateFakeArtist("bob"),af.GenerateFakeArtist("steve"),
            };

            SongsDto songDto = new SongsDto()
            {
                Songs = new List<SongWithLastPlayedDto> {
                    new SongWithLastPlayedDto
                    {
                        Id = 1,
                        AmountPlayed = 1,
                        name = "1",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "1",
                        Artists = new List<ArtistDto>
                        {
                            artists[0]
                        }
                    },

                    new SongWithLastPlayedDto
                    {
                        Id = 2,
                        AmountPlayed = 1,
                        name = "2",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "2",
                        Artists = new List<ArtistDto>
                        {
                            artists[1],artists[0]
                        }
                    },

                    new SongWithLastPlayedDto
                    {
                        Id = 3,
                        AmountPlayed = 1,
                        name = "3",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "3",
                        Artists = new List<ArtistDto>
                        {
                            artists[2]
                        }
                    },
                }
            };

            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
                new Result<bool>
                {
                    Data = true,
                }
            );
            A.CallTo(() => songRepository.GetSongsUsedByShow(A<int>._)).Returns(
                new Result<SongsDto>
                {
                    Data = songDto,
                }
            );

            Result<int> result = service.GetArtistPlayedCount(2, 0);

            Assert.False(result.IsFailed);
            Assert.Equal(1, result.Data);
        }

        [Fact]
        public void GetArtistPlayedCount_NoShow_showDoesNotExist()
        {
            ArtistFactory af = new ArtistFactory();
            List<ArtistDto> artists = new List<ArtistDto>
            {
                af.GenerateFakeArtist("jeff"), af.GenerateFakeArtist("bob"),af.GenerateFakeArtist("steve"),
            };

            SongsDto songDto = new SongsDto()
            {
                Songs = new List<SongWithLastPlayedDto> {
                    new SongWithLastPlayedDto
                    {
                        Id = 1,
                        AmountPlayed = 1,
                        name = "1",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "1",
                        Artists = new List<ArtistDto>
                        {
                            artists[0]
                        }
                    },

                    new SongWithLastPlayedDto
                    {
                        Id = 2,
                        AmountPlayed = 1,
                        name = "2",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "2",
                        Artists = new List<ArtistDto>
                        {
                            artists[1],artists[0]
                        }
                    },

                    new SongWithLastPlayedDto
                    {
                        Id = 3,
                        AmountPlayed = 1,
                        name = "3",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "3",
                        Artists = new List<ArtistDto>
                        {
                            artists[2]
                        }
                    },
                }
            };

            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
                new Result<bool>
                {
                    Data = false,
                }
            );
            A.CallTo(() => songRepository.GetSongsUsedByShow(A<int>._)).Returns(
                new Result<SongsDto>
                {
                    Data = songDto,
                }
            );

            Result<int> result = service.GetArtistPlayedCount(2, 0);

            Assert.True(result.IsFailed);
            Assert.True(result.IsFailedWarning);
            Assert.False(result.IsFailedError);
        }
        [Fact]
        public void GetArtistPlayedCount_NoShow_showError()
        {
            ArtistFactory af = new ArtistFactory();
            List<ArtistDto> artists = new List<ArtistDto>
            {
                af.GenerateFakeArtist("jeff"), af.GenerateFakeArtist("bob"),af.GenerateFakeArtist("steve"),
            };

            SongsDto songDto = new SongsDto()
            {
                Songs = new List<SongWithLastPlayedDto> {
                    new SongWithLastPlayedDto
                    {
                        Id = 1,
                        AmountPlayed = 1,
                        name = "1",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "1",
                        Artists = new List<ArtistDto>
                        {
                            artists[0]
                        }
                    },

                    new SongWithLastPlayedDto
                    {
                        Id = 2,
                        AmountPlayed = 1,
                        name = "2",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "2",
                        Artists = new List<ArtistDto>
                        {
                            artists[1],artists[0]
                        }
                    },

                    new SongWithLastPlayedDto
                    {
                        Id = 3,
                        AmountPlayed = 1,
                        name = "3",
                        LastPlayed = new DateTime(2010,10,10),
                        Release_date = new DateTime(2009,10,10),
                        User_description = "3",
                        Artists = new List<ArtistDto>
                        {
                            artists[2]
                        }
                    },
                }
            };

            A.CallTo(() => showRepository.DoesShowExist(A<int>._)).Returns(
                new Result<bool>
                {
                    ErrorMessage = "error",
                }
            ) ;
            A.CallTo(() => songRepository.GetSongsUsedByShow(A<int>._)).Returns(
                new Result<SongsDto>
                {
                    Data = songDto,
                }
            );

            Result<int> result = service.GetArtistPlayedCount(2, 0);

            Assert.True(result.IsFailed);
            Assert.False(result.IsFailedWarning);
            Assert.True(result.IsFailedError);
        }

        [Fact]
        public void getArtistsUsedInShow_happyflow_3ArtistReturned()
        {
            ArtistDto doubleArtist = new ArtistDto { Id = 1, name = "jeff" };
            List<int> idList = new List<int> { 1, 2, 3 };

            int showId = 1;

            A.CallTo(()=> showRepository.DoesShowExist(showId)).Returns(
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
            A.CallTo(() => artistRepository.GetArtistsListFromSongList(A<List<int>>._, A<int>._, A<int>._)).Returns(

                new Result<ArtistsDto>
                {
                    Data = new ArtistsDto
                    {
                        Artists = new List<ArtistDto>
                        {
                            new ArtistDto { Id = 2, name = "bob" },
                            new ArtistDto { Id = 2, name = "henk" },
                            doubleArtist
                        }
                    }
                }
            );

            Result<ArtistsDto> result = service.getArtistsUsedInShow(showId);

            Assert.True(result.Data.Artists.Count == 3);


        }

        [Fact]
        public void getArtistsUsedInShow_NoShow_WarningShowDoesntExist()
        {
            ArtistDto doubleArtist = new ArtistDto { Id = 1, name = "jeff" };
            List<int> idList = new List<int> { 1, 2, 3 };

            int showId = 1;

            A.CallTo(() => showRepository.DoesShowExist(showId)).Returns(
                new Result<bool>
                {
                    Data = false,
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
            A.CallTo(() => artistRepository.GetArtistsListFromSongList(A<List<int>>._, A<int>._, A<int>._)).Returns(

                new Result<ArtistsDto>
                {
                    Data = new ArtistsDto
                    {
                        Artists = new List<ArtistDto>
                        {
                            new ArtistDto { Id = 2, name = "bob" },
                            new ArtistDto { Id = 2, name = "henk" },
                            doubleArtist
                        }
                    }
                }
            );

            Result<ArtistsDto> result = service.getArtistsUsedInShow(showId);

            Assert.True(result.IsFailedWarning);


        }

        [Fact]
        public void getArtistsSearch_happyflow_Get3Artists()
        {
            ArtistFactory af = new ArtistFactory();
            List<ArtistDto> artists = new List<ArtistDto>
            {
                af.GenerateFakeArtist("jeff"), af.GenerateFakeArtist("jeffy"),af.GenerateFakeArtist("jeffsi"),
            };
            ArtistsDto dto = new ArtistsDto
            {
                Artists = artists
            };

            A.CallTo(() => artistRepository.GetArtistsForSearch("jeff")).Returns(
                new Result<ArtistsDto>
                {
                    Data = dto,
                }
            );

            Result<ArtistsDto> result = service.getArtistsSearch("jeff");

            Assert.False(result.IsFailed);
            Assert.Equal(3, result.Data.Artists.Count);
        }
        [Fact]
        public void getArtistsSearch_NoAristsfound_Get0Artists()
        {
            ArtistFactory af = new ArtistFactory();
            List<ArtistDto> artists = new List<ArtistDto>
            {
                //af.GenerateFakeArtist("jeff"), af.GenerateFakeArtist("jeffy"),af.GenerateFakeArtist("jeffsi"),
            };
            ArtistsDto dto = new ArtistsDto
            {
                Artists = artists
            };

            A.CallTo(() => artistRepository.GetArtistsForSearch("jeff")).Returns(
                new Result<ArtistsDto>
                {
                    Data = dto,
                }
            );

            Result<ArtistsDto> result = service.getArtistsSearch("jeff");

            Assert.False(result.IsFailed);
            Assert.Empty( result.Data.Artists);
        }
        [Fact]
        public void getArtistsSearch_Error_IsFailed()
        {
            ArtistFactory af = new ArtistFactory();
            List<ArtistDto> artists = new List<ArtistDto>
            {
                af.GenerateFakeArtist("jeff"), af.GenerateFakeArtist("jeffy"),af.GenerateFakeArtist("jeffsi"),
            };
            ArtistsDto dto = new ArtistsDto
            {
                Artists = artists
            };

            A.CallTo(() => artistRepository.GetArtistsForSearch("jeff")).Returns(
                new Result<ArtistsDto>
                {
                    ErrorMessage="isFailedError"
                }
            );

            Result<ArtistsDto> result = service.getArtistsSearch("jeff");

            Assert.True(result.IsFailed);
        }

    }
}