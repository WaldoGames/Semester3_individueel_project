using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Classes;
using Backend_DAL.Models;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core_unit_tests
{
    public class PlaylistTests
    {
        private PlayListService service;
        private IPlaylistRepository playlistRepository;
        private IShowRepository showRepository;
        private ISongRepository songRepository;
        public PlaylistTests()
        {
            TestVar.test = true;
            playlistRepository = A.Fake<IPlaylistRepository>();
            songRepository = A.Fake<ISongRepository>();
            showRepository = A.Fake<IShowRepository>();
            service = new PlayListService(playlistRepository, songRepository, showRepository);
        }

        [Fact]
        public void Getplaylist_Happyflow_getDto()
        {
            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).Returns(
               new NullableResult<PlayListDto>
               {
               }
            );
            SimpleResult result = service.GetPlaylist(1);
            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).MustHaveHappened();
        }

        [Fact]
        public void Getplaylists_Happyflow_getDto()
        {
            A.CallTo(() => playlistRepository.GetPlaylistsOverview(A<int>._)).Returns(
               new Result<PlaylistOverviewDto>
               {
               }
            );
            SimpleResult result = service.GetPlaylists(1);
            A.CallTo(() => playlistRepository.GetPlaylistsOverview(A<int>._)).MustHaveHappened();
        }
        [Fact]
        public void WebGetPlaylistStatus_Happyflow_GetPlaylistStatusWithoutSong()
        {
            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).Returns(
               new NullableResult<PlayListDto>
               {
                   Data = new PlayListDto()
                   {
                       Id = 1,
                       creatorId = 1,
                       recordingPlayListName = "name",
                       items = new List<PlayListItemDto>
                       {
                           new PlayListItemDto()
                           {
                               discription="song1",
                               orderIndex=0,
                               playlistId=1,
                               Id=1
                           },
                           new PlayListItemDto()
                           {
                               discription="song2",
                               orderIndex=1,
                               playlistId=1,
                               playlistItemSongId=1,
                               Id=2
                           },
                           new PlayListItemDto()
                           {
                               discription="song3",
                               orderIndex=2,
                               playlistId=1,
                               Id=3
                           }
                       }

                   }
               }
            );
            A.CallTo(() => songRepository.GetSong(A<int>._)).Returns(
               new NullableResult<SongDto>
               {
                   Data = new SongDto()
                   {
                       Id = 1,
                       Artists = new List<ArtistDto>
                        {
                            new ArtistDto() {
                             Id = 1,
                             name="artist"}
                        },
                       name = "name",
                       Release_date = new DateTime(2007, 3, 3)

                   }
               }
            );
            A.CallTo(() => showRepository.GetShowDiscriptionOfSong(A<int>._, A<int>._)).Returns(
               new NullableResult<string>
               {
                   Data = "this is a good song"
               }
            );


            Result<PlaylistStatusDto> result = service.WebGetPlaylistStatus(1, 1, 0);

            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).MustHaveHappened();

            Assert.False(result.IsFailed);
            Assert.True(result.Data.FirstItem);
            Assert.False(result.Data.LastItem);
            Assert.Equal("song1", result.Data.currentItem.discription);
            Assert.Null(result.Data.currentItem.song);
        }
        [Fact]
        public void WebGetPlaylistStatus_Happyflow_GetPlaylistStatusWithSong()
        {
            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).Returns(
               new NullableResult<PlayListDto>
               {
                   Data = new PlayListDto()
                   {
                       Id = 1,
                       creatorId = 1,
                       recordingPlayListName = "name",
                       items = new List<PlayListItemDto>
                       {
                           new PlayListItemDto()
                           {
                               discription="item1",
                               orderIndex=0,
                               playlistId=1,
                               Id=1
                           },
                           new PlayListItemDto()
                           {
                               discription="item2",
                               orderIndex=1,
                               playlistId=1,
                               playlistItemSongId=1,
                               Id=2
                           },
                           new PlayListItemDto()
                           {
                               discription="item3",
                               orderIndex=2,
                               playlistId=1,
                               Id=3
                           }
                       }

                   }
               }
            );
            A.CallTo(() => songRepository.GetSong(A<int>._)).Returns(
               new NullableResult<SongDto>
               {
                   Data = new SongDto()
                   {
                       Id = 1,
                       Artists = new List<ArtistDto>
                        {
                            new ArtistDto() {
                             Id = 1,
                             name="artist"}
                        },
                       name = "song",
                       Release_date = new DateTime(2007, 3, 3)

                   }
               }
            );
            A.CallTo(() => showRepository.GetShowDiscriptionOfSong(A<int>._, A<int>._)).Returns(
               new NullableResult<string>
               {
                   Data = "this is a good song"
               }
            );


            Result<PlaylistStatusDto> result = service.WebGetPlaylistStatus(1, 1, 1);

            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).MustHaveHappened();

            Assert.False(result.IsFailed);
            Assert.False(result.Data.FirstItem);
            Assert.False(result.Data.LastItem);
            Assert.Equal("item2", result.Data.currentItem.discription);
            Assert.NotNull(result.Data.currentItem.song);
            Assert.Equal("song", result.Data.currentItem.song.name);
            Assert.Equal("this is a good song", result.Data.currentItem.song.User_description);
        }

        [Fact]
        public void WebGetPlaylistStatus_CreatorIdMissmatch_getWarningError()
        {
            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).Returns(
               new NullableResult<PlayListDto>
               {
                   Data = new PlayListDto()
                   {
                       Id = 1,
                       creatorId = 2,
                       recordingPlayListName = "name",
                       items = new List<PlayListItemDto>
                       {
                           new PlayListItemDto()
                           {
                               discription="item1",
                               orderIndex=0,
                               playlistId=1,
                               Id=1
                           },
                           new PlayListItemDto()
                           {
                               discription="item2",
                               orderIndex=1,
                               playlistId=1,
                               playlistItemSongId=1,
                               Id=2
                           },
                           new PlayListItemDto()
                           {
                               discription="item3",
                               orderIndex=2,
                               playlistId=1,
                               Id=3
                           }
                       }

                   }
               }
            );
            A.CallTo(() => songRepository.GetSong(A<int>._)).Returns(
               new NullableResult<SongDto>
               {
                   Data = new SongDto()
                   {
                       Id = 1,
                       Artists = new List<ArtistDto>
                        {
                            new ArtistDto() {
                             Id = 1,
                             name="artist"}
                        },
                       name = "song",
                       Release_date = new DateTime(2007, 3, 3)

                   }
               }
            );
            A.CallTo(() => showRepository.GetShowDiscriptionOfSong(A<int>._, A<int>._)).Returns(
               new NullableResult<string>
               {
                   Data = "this is a good song"
               }
            );


            Result<PlaylistStatusDto> result = service.WebGetPlaylistStatus(1, 1, 1);

            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).MustHaveHappened();

            Assert.True(result.IsFailed);
            Assert.True(result.IsFailedWarning);
            Assert.False(result.IsFailedError);
        }

        [Fact]
        public void WebGetPlaylistStatus_WrongShowId_ErrorWarning()
        {
            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).Returns(
               new NullableResult<PlayListDto>
               {
                   Data = new PlayListDto()
                   {
                       Id = 1,
                       creatorId = 2,
                       recordingPlayListName = "name",
                       items = new List<PlayListItemDto>
                       {
                           new PlayListItemDto()
                           {
                               discription="item1",
                               orderIndex=0,
                               playlistId=1,
                               Id=1
                           },
                           new PlayListItemDto()
                           {
                               discription="item2",
                               orderIndex=1,
                               playlistId=1,
                               playlistItemSongId=1,
                               Id=2
                           },
                           new PlayListItemDto()
                           {
                               discription="item3",
                               orderIndex=2,
                               playlistId=1,
                               Id=3
                           }
                       }

                   }
               }
            );
            A.CallTo(() => songRepository.GetSong(A<int>._)).Returns(
               new NullableResult<SongDto>
               {
                   Data = new SongDto()
                   {
                       Id = 1,
                       Artists = new List<ArtistDto>
                        {
                            new ArtistDto() {
                             Id = 1,
                             name="artist"}
                        },
                       name = "song",
                       Release_date = new DateTime(2007, 3, 3)

                   }
               }
            );
            A.CallTo(() => showRepository.GetShowDiscriptionOfSong(A<int>._, A<int>._)).Returns(
               new NullableResult<string>
               {
                   Data = "this is a good song"
               }
            );


            Result<PlaylistStatusDto> result = service.WebGetPlaylistStatus(1, 1, 1);

            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).MustHaveHappened();

            Assert.True(result.IsFailed);
            Assert.True(result.IsFailedWarning);
            Assert.False(result.IsFailedError);
        }
        [Fact]
        public void WebGetPlaylistStatus_FailedToGetPlaylistResult_returnError()
        {
            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).Returns(
               new NullableResult<PlayListDto>
               {
                   ErrorMessage = ""
               }
            );
            A.CallTo(() => songRepository.GetSong(A<int>._)).Returns(
               new NullableResult<SongDto>
               {
                   Data = new SongDto()
                   {
                       Id = 1,
                       Artists = new List<ArtistDto>
                        {
                            new ArtistDto() {
                             Id = 1,
                             name="artist"}
                        },
                       name = "song",
                       Release_date = new DateTime(2007, 3, 3)

                   }
               }
            );
            A.CallTo(() => showRepository.GetShowDiscriptionOfSong(A<int>._, A<int>._)).Returns(
               new NullableResult<string>
               {
                   Data = "this is a good song"
               }
            );


            Result<PlaylistStatusDto> result = service.WebGetPlaylistStatus(1, 1, 1);

            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).MustHaveHappened();

            Assert.True(result.IsFailed);
            Assert.False(result.IsFailedWarning);
            Assert.True(result.IsFailedError);
        }

        [Fact]
        public void CreatePlaylist_Happyflow_createAPlaylist()
        {
            A.CallTo(() => playlistRepository.CreatePlaylist(A<NewPlaylistDto>._)).Returns(
               new Result<int>
               {
                   Data = 1
               }
            );
            A.CallTo(() => playlistRepository.CreatePlaylistItem(A<NewPlaylistItemDto>._)).Returns(
               new SimpleResult
               {

               }
            );
            SimpleResult result = service.CreatePlaylist(new NewPlaylistDto
            {
                playListDescription = "pl",
                recordingPlayListName = "plname",
                ShowId = 1,
                playlistItems = new List<NewPlaylistItemDto>
                {
                    new NewPlaylistItemDto{
                        description="item",
                        orderIndex=0,
                        playlistId=1,
                    }
                }
            });
            Assert.False(result.IsFailed);
        }

        [Fact]
        public void CreatePlaylist_CantCreatePlaylist_GetError()
        {
            A.CallTo(() => playlistRepository.CreatePlaylist(A<NewPlaylistDto>._)).Returns(
               new Result<int>
               {
                   ErrorMessage = "oh no"
               }
            );
            A.CallTo(() => playlistRepository.CreatePlaylistItem(A<NewPlaylistItemDto>._)).Returns(
               new SimpleResult
               {

               }
            );
            SimpleResult result = service.CreatePlaylist(new NewPlaylistDto { });
            Assert.True(result.IsFailed);
            Assert.True(result.IsFailedError);
            Assert.False(result.IsFailedWarning);
        }

        [Fact]
        public void CreatePlaylist_CantCreatePlaylistItem_GetError()
        {
            A.CallTo(() => playlistRepository.CreatePlaylist(A<NewPlaylistDto>._)).Returns(
               new Result<int>
               {

               }
            );
            A.CallTo(() => playlistRepository.CreatePlaylistItem(A<NewPlaylistItemDto>._)).Returns(
               new SimpleResult
               {
                   ErrorMessage = "oh no"
               }
            );
            SimpleResult result = service.CreatePlaylist(new NewPlaylistDto
            {
                playListDescription = "pl",
                recordingPlayListName = "plname",
                ShowId = 1,
                playlistItems = new List<NewPlaylistItemDto>
                {
                    new NewPlaylistItemDto{
                        description="item",
                        orderIndex=0,
                        playlistId=1,
                    }
                }
            });
            Assert.True(result.IsFailed);
            Assert.True(result.IsFailedError);
            Assert.False(result.IsFailedWarning);
        }

        [Fact]
        public void ResetPlayListOrderIndex_Happyflow_orderedList()
        {
            List<PlayListItemDto> playlistItems = new List<PlayListItemDto>
                {
                    new PlayListItemDto{
                        discription="item",
                        orderIndex=2,
                        playlistId=1,
                    },
                    new PlayListItemDto{
                        discription="item2",
                        orderIndex=1,
                        playlistId=1,
                    },
                    new PlayListItemDto{
                        discription="item3",
                        orderIndex=0,
                        playlistId=1,
                    }
                };
            service.ResetPlayListOrderIndex(playlistItems);

            Assert.Equal(0, playlistItems[0].orderIndex);
            Assert.Equal(1, playlistItems[1].orderIndex);
            Assert.Equal(2, playlistItems[2].orderIndex);

        }
    }
}
