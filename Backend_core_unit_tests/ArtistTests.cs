using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using FakeItEasy;

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
                            Creators = new List<ArtistDto> { new ArtistDto { Id = 2, name = "bob" }, doubleArtist } },

                                new SongWithLastPlayedDto { Id = 0, Release_date = DateTime.Now, name = "song2guess",
                            Creators = new List<ArtistDto> { new ArtistDto { Id = 2, name = "henk" }, doubleArtist } }
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
                            Creators = new List<ArtistDto> { new ArtistDto { Id = 2, name = "bob" }, doubleArtist } },

                                new SongWithLastPlayedDto { Id = 0, Release_date = DateTime.Now, name = "song2guess",
                            Creators = new List<ArtistDto> { new ArtistDto { Id = 2, name = "henk" }, doubleArtist } }
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


    }
}