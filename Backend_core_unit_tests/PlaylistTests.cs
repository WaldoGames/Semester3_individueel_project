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
        public PlaylistTests()
        {
            TestVar.test = true;
            playlistRepository = A.Fake<IPlaylistRepository>();
        }

        [Fact]
        {
               {
        {
               {
                           },
                           {
                           },
                           {
                           }
                       
               {
                   }
               }
            );
               {
                   Data= "this is a good song"
               }
            );

            SimpleResult result = service.CreatePlaylist(dto);

            Result<PlaylistStatusDto> result = service.WebGetPlaylistStatus(1, 1, 0);

            A.CallTo(() => playlistRepository.GetPlaylist(A<int>._)).MustHaveHappened();

            Assert.False(result.IsFailed);
        }
        [Fact]
        {
               {
                   {
                           {
                           },
                           {
                           },
                        {
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
            };

                           {
               }
            );
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

            SimpleResult result = service.CreatePlaylist(dto);

            Assert.True(result.IsFailed);
        }

        [Fact]
        {
               {
                   {
                       {
                           },
                           {
                           },
                           {
                        },
                       name = "song",
                       Release_date = new DateTime(2007, 3, 3)

                   }

               new Result<int>
               {
               }
            );
               new SimpleResult
               {
               }
            );
            SimpleResult result = service.CreatePlaylist(new NewPlaylistDto { });
            Assert.True(result.IsFailed);
            Assert.True(result.IsFailedError);
            Assert.False(result.IsFailedWarning);
        }

                   
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
